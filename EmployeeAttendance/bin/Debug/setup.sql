create table attendance(
	no INTEGER PRIMARY KEY,  
	ename   varchar(15),
	cdate   varchar(8),   -- YYYYMMDD
    timein  varchar(18),  -- YYYY-MM-DD HH:MM:SS
    timeout varchar(18),  -- YYYY-MM-DD HH:MM:SS
    elapse  INTEGER       -- interval between timein and timeout in seconds   
);

create table worklog(
	no INTEGER PRIMARY KEY,  
	ename   varchar(15),
	inTime   varchar(10),   
    outTime  varchar(10),  
    elapse  INTEGER       -- interval between timein and timeout in seconds   
);

