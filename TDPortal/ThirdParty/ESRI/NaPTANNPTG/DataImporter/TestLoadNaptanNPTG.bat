	echo *** importing nptgdataprepare.xml using dataimporter.exe ***   					>> %OUTPUT_FILE%

	D:\gateway\bin\utils\naptannptg\dataimporter\dataimporter.exe -c "D:\gateway\bin\utils\naptannptg\dataimporter\Configuration\LoadAndPrepareNaptanAndNPTG.xml"  		>> D:\Gateway\bin\Utils\NaPTANNPTG\DataImporter\testRun.log
