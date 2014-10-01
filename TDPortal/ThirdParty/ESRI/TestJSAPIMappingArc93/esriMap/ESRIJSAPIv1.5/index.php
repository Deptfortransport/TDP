<?php
header("Content-type:application/x-javascript");
echo "if (typeof dojo == \"undefined\") {";
readfile("js/dojo/dojo/dojo.xd.js");
echo "}";
readfile("js/esri/esri.js");
readfile("js/esri/jsapi.js");
?>