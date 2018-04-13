PROMPT CREATE OR REPLACE PROCEDURE dump_table_to_csv
CREATE OR REPLACE procedure DUMP_TABLE_TO_CSV(
  p_tname IN VARCHAR2,
  p_dir IN VARCHAR2,
  p_filename IN VARCHAR2 ,
  p_location IN VARCHAR2,
  p_extralocationfilter IN VARCHAR2,
  p_selectexpression IN VARCHAR2)
 IS
 l_output UTL_FILE.file_type;
 l_theCursor INTEGER DEFAULT DBMS_SQL.open_cursor;
 l_columnValue VARCHAR2(4000);
 l_status INTEGER;
 l_query VARCHAR2(10000);
 l_colCnt NUMBER ;
 l_separator VARCHAR2(3);
 l_descTbl dbms_sql.desc_tab;
 l_skiplocation NUMBER := 0;
 BEGIN

      IF p_location IS NULL THEN
          l_query := p_selectexpression;
      ELSIF p_extralocationfilter IS NULL THEN
          l_query := p_selectexpression || '''' || p_location || '''';
      ELSE
          l_query := p_selectexpression || '''' || p_location || '''' || ')';
      END IF;

    l_output := UTL_FILE.fopen( p_dir, p_filename, 'w', 2000 );
    execute immediate 'alter session set nls_date_format=''dd-mon-yyyy hh24:mi:ss'' ';
    DBMS_SQL.parse( l_theCursor, l_query, DBMS_SQL.native );
    DBMS_SQL.describe_columns( l_theCursor, l_colCnt, l_descTbl );

    FOR i IN 1 .. l_colCnt LOOP
      UTL_FILE.put( l_output, l_separator || '"' || l_descTbl(i).col_name || '"' );
      l_separator := ',';
      DBMS_SQL.define_column( l_theCursor, i, l_columnValue, 4000 );
    END LOOP;
    UTL_FILE.new_line( l_output );

    l_status := DBMS_SQL.execute(l_theCursor);

    while ( DBMS_SQL.fetch_rows(l_theCursor) > 0 ) loop
      FOR i IN 1 .. l_colCnt LOOP
          DBMS_SQL.column_value( l_theCursor, i, l_columnValue );
          IF INSTR(l_columnValue, CHR(10)) != 0 || INSTR(l_columnValue, CHR(13)) THEN
             SELECT REPLACE(REPLACE( l_columnValue, CHR(10), '~' ), CHR(13) ) INTO l_columnValue FROM dual;
          END IF;
          IF (i = l_colCnt) THEN
             UTL_FILE.put( l_output, '"' || l_columnValue || '"' );
          ELSE
             UTL_FILE.put( l_output, '"' || l_columnValue || '",' );
          END IF;
      END LOOP;
      UTL_FILE.new_line( l_output );
    END LOOP;
  DBMS_SQL.close_cursor(l_theCursor);
  UTL_FILE.fclose( l_output );
  l_output := UTL_FILE.fopen( p_dir, p_filename, 'ab');
  UTL_FILE.put_raw(l_output, UTL_RAW.cast_to_raw('"'));
  UTL_FILE.fclose( l_output );
  EXECUTE IMMEDIATE 'alter session set nls_date_format=''dd-MON-yy'' ';
 EXCEPTION
    WHEN OTHERS THEN
      IF UTL_FILE.is_open (l_output) THEN
        UTL_FILE.fclose (l_output);
      END IF;
      EXECUTE IMMEDIATE 'alter session set nls_date_format=''dd-MON-yy'' ';
      RAISE;
 END;
/

