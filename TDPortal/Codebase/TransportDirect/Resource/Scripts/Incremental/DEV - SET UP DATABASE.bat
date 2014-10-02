@echo off

@echo **************************************************
@echo DEV SETTING UP DATABASES
@echo **************************************************

@echo Calling batch files

cd "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental"

call "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\IncrementalUpdates.bat" < nul
call "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\SoftContentUpdates.bat" < nul
call "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\MDSScripts.bat" < nul
call "D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\TransportDirect2Scripts.bat" < nul

osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\DEV_01_DatabaseUpdate.sql"
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\DEV_02_StoredProceduresUpdate.sql" 
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\DEV_03_DatabasePermissions.sql"
osql -S .\SQLExpress -Esa -i"D:\TDPortal\Codebase\TransportDirect\Resource\Scripts\Incremental\DEV_04_Properties_Update.sql"


@echo **************************************************
@echo Finished
@echo **************************************************

pause
pause