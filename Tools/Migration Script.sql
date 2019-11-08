use F00007430_DMS;

select ut_name, ut_codefld, ut_descfld, ut_where from DA_TYPE 
where 
	ut_name is not null and (upper(cast(ut_name as varchar(255))) like '%{SCHEMA}%' or upper(cast(ut_name as varchar(255))) like '%PROARC%') or
	(							
		upper(cast(ut_where as varchar(255))) like '%DECODE%' or 					
		upper(cast(ut_where as varchar(255))) like '%TO_CHAR%' or					
		upper(cast(ut_where as varchar(255))) like '%(+)%' or					
		upper(cast(ut_where as varchar(255))) like '%ROWNUM%'  or					
		upper(cast(ut_where as varchar(255))) like '%SUBSTR%' 	or
		upper(cast(ut_where as varchar(255))) like '%LPAD%' or
		upper(cast(ut_where as varchar(255))) like '%RPAD%' or 
		upper(cast(ut_where as varchar(255))) like '%CEIL%' or
		upper(cast(ut_where as varchar(255))) like '%TRUNC%' or
		upper(cast(ut_where as varchar(255))) like '%NVL%' or
		upper(cast(ut_where as varchar(255))) like '%CONCAT%' or
		upper(cast(ut_where as varchar(255))) like '%CHR%' or
		upper(cast(ut_where as varchar(255))) like '%INSTR%' or
		upper(cast(ut_where as varchar(255))) like '%TO_NUMBER%' or
		upper(cast(ut_where as varchar(255))) like '%TO_DATE%' or
		upper(cast(ut_where as varchar(255))) like '%SYSDATE%' or
		upper(cast(ut_where as varchar(255))) like '%ROUND%' or
		upper(cast(ut_where as varchar(255))) like '%||%' or 
		upper(cast(ut_where as varchar(255))) like '%PROARC%' or 		
		upper(cast(ut_where as varchar(255))) like '%{SCHEMA}%' or
		upper(cast(ut_where as varchar(255))) like '%DUAL%' 
	)

/*	
update da_type set UT_NAME = replace(cast(ut_name as varchar(500)), '{schema}', 'dbo.')
update da_type set UT_NAME = replace(cast(ut_name as varchar(500)), '_PROARC.', '_DMS.dbo.') where UT_NAME like '%_PROARC.%'
update da_type set UT_WHERE = replace(cast(ut_where as varchar(500)), '{schema}', 'dbo.')
update da_type set UT_WHERE = replace(cast(ut_where as varchar(500)), '_PROARC.', '_DMS.dbo.') where UT_WHERE like '%_PROARC.%'

update da_type set UT_WHERE = replace(cast(ut_where as varchar(500)), 'substr', 'substring') where ut_where like '%SUBSTR%'
*/

select di_action							
from da_init 
where 
	(upper(cast(DI_ACTION as varchar(255))) like '%{SCHEMA}%' or upper(cast(DI_ACTION as varchar(255))) like '%PROARC%') or 
	(							
		upper(cast(DI_ACTION as varchar(255))) like '%DECODE%' or 					
		upper(cast(DI_ACTION as varchar(255))) like '%TO_CHAR%' or					
		upper(cast(DI_ACTION as varchar(255))) like '%(+)%' or					
		upper(cast(DI_ACTION as varchar(255))) like '%ROWNUM%'  or					
		upper(cast(DI_ACTION as varchar(255))) like '%SUBSTR%' 	or
		upper(cast(DI_ACTION as varchar(255))) like '%LPAD%' or
		upper(cast(DI_ACTION as varchar(255))) like '%RPAD%' or 
		upper(cast(DI_ACTION as varchar(255))) like '%CEIL%' or
		upper(cast(DI_ACTION as varchar(255))) like '%TRUNC%' or
		upper(cast(DI_ACTION as varchar(255))) like '%NVL%' or
		upper(cast(DI_ACTION as varchar(255))) like '%CONCAT%' or
		upper(cast(DI_ACTION as varchar(255))) like '%CHR%' or
		upper(cast(DI_ACTION as varchar(255))) like '%INSTR%' or
		upper(cast(DI_ACTION as varchar(255))) like '%TO_NUMBER%' or
		upper(cast(DI_ACTION as varchar(255))) like '%TO_DATE%' or
		upper(cast(DI_ACTION as varchar(255))) like '%SYSDATE%' or
		upper(cast(DI_ACTION as varchar(255))) like '%ROUND%' or
		upper(cast(DI_ACTION as varchar(255))) like '%||%' 	or
		upper(cast(DI_ACTION as varchar(255))) like '%PROARC%' or 		
		upper(cast(DI_ACTION as varchar(255))) like '%{SCHEMA}%' or
		upper(cast(DI_ACTION as varchar(255))) like '%DUAL%'  			
	)


/*
update da_init set di_action = replace(cast(di_action as varchar(500)), '{schema}', 'dbo.')
update da_init set di_action = replace(cast(di_action as varchar(500)), '_PROARC.', '_DMS.dbo.') where di_action like '%_PROARC.%'
update da_init set di_action = replace(cast(di_action as varchar(500)), 'where rownum = 1', '') where di_action like '%ROWNUM%'

update da_init set di_action = replace(cast(di_action as varchar(500)), 'DECODE(r.supplier_revision, NULL, ''-'', r.supplier_revision )', 'isnull(r.supplier_revision,''-'')') where di_action like '%DECODE%'
update da_init set di_action = replace(cast(di_action as varchar(500)), 'to_char(sysdate,''YY'')', 'RIGHT(CONVERT(VARCHAR(4), GETDATE(), 112),2) ') where di_action like '%TO_CHAR%'
update da_init set di_action = replace(cast(di_action as varchar(500)), 'to_char(created,''YY'')', 'RIGHT(CONVERT(VARCHAR(4), CREATED, 112),2) ') where di_action like '%TO_CHAR%'
update da_init set di_action = replace(cast(di_action as varchar(500)), 'lpad(count(*)+1,6,''0'')', 'right(replicate(''0'',6) + cast((count(*)+1) as varchar(6)),6)') where di_action like '%LPAD%'
update da_init set di_action = replace(cast(di_action as varchar(500)), 'substr', 'substring') where di_action like '%SUBSTR%'
update da_init set di_action = replace(cast(di_action as varchar(500)), 'nvl(SA_SAK_AVSLUTTET,0)', 'isnull(SA_SAK_AVSLUTTET,0)') where di_action like '%NVL%'
update da_init set di_action = replace(cast(di_action as varchar(500)), '||', '+') where di_action like '%||%'

update da_init set di_action = replace(cast(di_action as varchar(500)), ',sysdate,', ',getdate(),') where upper(cast(DI_ACTION as varchar(255))) like '%SYSDATE%'
*/

select * from LS_LISTSETUP
where upper(cast(LS_EXP as varchar(255))) like '%{SCHEMA}%' or 
      upper(cast(LS_EXP as varchar(255))) like '%_PROARC.%' or
	(							
		upper(cast(LS_EXP as varchar(255))) like '%DECODE%' or 					
		upper(cast(LS_EXP as varchar(255))) like '%TO_CHAR%' or					
		upper(cast(LS_EXP as varchar(255))) like '%(+)%' or					
		upper(cast(LS_EXP as varchar(255))) like '%ROWNUM%'  or					
		upper(cast(LS_EXP as varchar(255))) like '%SUBSTR%' 	or
		upper(cast(LS_EXP as varchar(255))) like '%LPAD%' or
		upper(cast(LS_EXP as varchar(255))) like '%RPAD%' or 
		upper(cast(LS_EXP as varchar(255))) like '%CEIL%' or
		upper(cast(LS_EXP as varchar(255))) like '%TRUNC%' or
		upper(cast(LS_EXP as varchar(255))) like '%NVL%' or
		upper(cast(LS_EXP as varchar(255))) like '%CONCAT%' or
		upper(cast(LS_EXP as varchar(255))) like '%CHR%' or
		upper(cast(LS_EXP as varchar(255))) like '%INSTR%' or
		upper(cast(LS_EXP as varchar(255))) like '%TO_NUMBER%' or
		upper(cast(LS_EXP as varchar(255))) like '%TO_DATE%' or
		upper(cast(LS_EXP as varchar(255))) like '%SYSDATE%' or
		upper(cast(LS_EXP as varchar(255))) like '%ROUND%' or
		upper(cast(LS_EXP as varchar(255))) like '%||%' or
		upper(cast(LS_EXP as varchar(255))) like '%PROARC%' or 		
		upper(cast(LS_EXP as varchar(255))) like '%{SCHEMA}%' or
		upper(cast(LS_EXP as varchar(255))) like '%DUAL%'  				
	)

/*
update LS_LISTSETUP set LS_EXP = replace(cast(LS_EXP as varchar(500)), '{schema}', 'dbo.')
*/

select * from SQL_SCRIPT 
where ss_sql is not null and
	upper(cast(ss_sql as varchar(255))) like '%{SCHEMA}%' or 
	upper(cast(ss_sql as varchar(255))) like '%_PROARC.%' or
	(							
		upper(cast(ss_sql as varchar(255))) like '%DECODE%' or 					
		upper(cast(ss_sql as varchar(255))) like '%TO_CHAR%' or					
		upper(cast(ss_sql as varchar(255))) like '%(+)%' or					
		upper(cast(ss_sql as varchar(255))) like '%ROWNUM%'  or					
		upper(cast(ss_sql as varchar(255))) like '%SUBSTR%' 	or
		upper(cast(ss_sql as varchar(255))) like '%LPAD%' or
		upper(cast(ss_sql as varchar(255))) like '%RPAD%' or 
		upper(cast(ss_sql as varchar(255))) like '%CEIL%' or
		upper(cast(ss_sql as varchar(255))) like '%TRUNC%' or
		upper(cast(ss_sql as varchar(255))) like '%NVL%' or
		upper(cast(ss_sql as varchar(255))) like '%CONCAT%' or
		upper(cast(ss_sql as varchar(255))) like '%CHR%' or
		upper(cast(ss_sql as varchar(255))) like '%INSTR%' or
		upper(cast(ss_sql as varchar(255))) like '%TO_NUMBER%' or
		upper(cast(ss_sql as varchar(255))) like '%TO_DATE%' or
		upper(cast(ss_sql as varchar(255))) like '%SYSDATE%' or
		upper(cast(ss_sql as varchar(255))) like '%ROUND%' or
		upper(cast(ss_sql as varchar(255))) like '%||%' or
		upper(cast(ss_sql as varchar(255))) like '%PROARC%' or 		
		upper(cast(ss_sql as varchar(255))) like '%{SCHEMA}%' or
		upper(cast(ss_sql as varchar(255))) like '%DUAL%'  				
	)
	
/*
update SQL_SCRIPT set SS_SQL = replace(cast(SS_SQL as varchar(1000)), '{schema}', 'dbo.')
update SQL_SCRIPT set ss_sql = replace(cast(ss_sql as varchar(1000)), 'to_date(sysdate + res_nodays)', 'convert(varchar(9),(GETDATE() + res_nodays),6) ') where upper(cast(ss_sql as varchar(1000))) like '%TO_DATE%'	

update SQL_SCRIPT set SS_SQL = replace(cast(SS_SQL as varchar(1000)), ' B set ', ' set ') where  upper(cast(ss_sql as varchar(1000))) like '% B set %'
update SQL_SCRIPT set SS_SQL = replace(upper(cast(SS_SQL as varchar(1000))), 'B.', '') where  upper(cast(ss_sql as varchar(1000))) like '%B.%'
update SQL_SCRIPT set SS_SQL = replace(upper(cast(SS_SQL as varchar(1000))), 'from dual', '') where  upper(cast(ss_sql as varchar(1000))) like '%FROM DUAL%' 
*/

--SKIP THE UPDATE OF REPORT
select rep_report, rep_filename, replace(rep_report,'642','671'), replace(rep_filename,'642','671') from REP_REPORT

/*
update REP_REPORT set rep_report = replace(rep_report,'642','671'), rep_filename = replace(rep_filename,'642','671')
*/


/*
NOTES:
1. select max(change_event_rno) from changeevent
   select * from dbo.sequences -- update using the max from changeevent
   update dbo.sequences set sq_count = (select max(change_event_rno) from changeevent) where sq_name = 'CHANGE_RNO'

2. in prsetup.ini -remove (from dual) sql syntax

3. edit TRG_WEB_SETTINGS and modify the var_schema 

4. Set the DMS workflow password to blank

5. Verify view PO and PO1 can retrieve values - check columns are correct - drop and recreate view 
*/