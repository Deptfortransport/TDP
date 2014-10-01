@echo off
rem ************************************************************************************
rem * The Purpose of this batch file is to check the presence of dlfa.dat, dllu.dat,   *
rem * dlre.dat, dlra.dat, dlsu.dat files in D:\RBOData folder after 11:00 AM and send  *
rem * a console message to TDPTNG03 to raise an alert for UK.WebSupport.Nott team      *
rem *										       *
rem * Shafiulla Azad - TNG Support - 22/08/2007					       *
rem ************************************************************************************





if exist D:\RBOData\dlfa.dat (cawto -n NSM01 -c red The file D:\RBOData\dlfa.dat used for FBO feed Still exists on AP01 after 11:00 AM)
if exist D:\RBOData\dllu.dat (cawto -n NSM01 -c red The file D:\RBOData\dllu.dat used for LBO feed Still exists on AP01 after 11:00 AM)
if exist D:\RBOData\dlre.dat (cawto -n NSM01 -c red The file D:\RBOData\dlre.dat used for RBO feed Still exists on AP01 after 11:00 AM)
if exist D:\RBOData\dlra.dat (cawto -n NSM01 -c red The file D:\RBOData\dlra.dat used for RVBO feed Still exists on AP01 after 11:00 AM)
if exist D:\RBOData\dlsu.dat (cawto -n NSM01 -c red The file D:\RBOData\dlsu.dat used for SBO feed Still exists on AP01 after 11:00 AM)

if exist D:\RBOData\dlfa.dat (cawto -n NSM02 -c red The file D:\RBOData\dlfa.dat used for FBO feed Still exists on AP01 after 11:00 AM)
if exist D:\RBOData\dllu.dat (cawto -n NSM02 -c red The file D:\RBOData\dllu.dat used for LBO feed Still exists on AP01 after 11:00 AM)
if exist D:\RBOData\dlre.dat (cawto -n NSM02 -c red The file D:\RBOData\dlre.dat used for RBO feed Still exists on AP01 after 11:00 AM)
if exist D:\RBOData\dlra.dat (cawto -n NSM02 -c red The file D:\RBOData\dlra.dat used for RVBO feed Still exists on AP01 after 11:00 AM)
if exist D:\RBOData\dlsu.dat (cawto -n NSM02 -c red The file D:\RBOData\dlsu.dat used for SBO feed Still exists on AP01 after 11:00 AM)