// InternationalDataDeclarations file generated at 23/4/2010 14:29.17
var cities = new Array("1", "2", "3", "4", "5");
var cityNames;
var cityRouteTable;

populateInternationalCityDataArrays();

function populateInternationalCityDataArrays()
{
cityNames = new Array();
cityRouteTable = new Array();

cityNames["1"] = "London";
cityRouteTable["1"] = new Array("2", "3", "4", "5");
cityNames["2"] = "Brussels";
cityRouteTable["2"] = new Array("1");
cityNames["3"] = "Calais";
cityRouteTable["3"] = new Array("1");
cityNames["4"] = "Lille";
cityRouteTable["4"] = new Array("1");
cityNames["5"] = "Paris";
cityRouteTable["5"] = new Array("1");
}
