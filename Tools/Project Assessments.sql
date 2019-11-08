--Notes: Change the database name of the project to be assess
ALTER SESSION SET CURRENT_SCHEMA = F01013246_PROARC;

--External Validation
select count(*) from DA_TYPE where ut_name is not null;

--Initialization
select count(*) from DA_INIT;

--Result List
select count(*) from LS_LISTSETUP;

--Report
select count(*) from REP_REPORT;

--Trigger Count
select count(*) from dba_triggers where owner = 'F01013246_PROARC';

--Views
select count(*) from dba_views where owner = 'F01013246_PROARC';

--Workflow
select count(*) from SQL_SCRIPT;

--Archieve Folders
select (select DB_ID from db_grp where db_rno = d.db_rno and rownum < 2) db_id/*, d.db_rno*/, count(*) total_docs from document d
group by db_rno;

select * from REP_REPORT;
select * from dba_triggers where owner = 'F01013246_PROARC';
select * from dba_views where owner = 'F01013246_PROARC';

--Initialization of Fields (Eng, Projexec)
Select Name, DI_Event, DI_Target, DI_Always, DI_Action  From DA_INIT Where DB_rno = 2 order by DI_Seqno;
Select Name, DI_Event, DI_Target, DI_Always, DI_Action  From DA_INIT Where DB_rno = 12 order by DI_Seqno;





