// ***********************************************
// NAME     : MapInputControl.js
// AUTHOR   : Atos Origin
// ************************************************

// Max scale at which bus symbols should be visible
var maxBusSymbolMapScale = 4000;

function setTransportSymbolsOnMap(/*id of the map*/mapid, /* comma separated string specifing transport symbols */symbols) {
    var layerState = null;

    var map = findMap(mapid);

    if (map && symbols) {
        try {

            layerState = map.getLayerList();

            var symbolsArr = symbols.split(",");

            for (var symbolCount in symbolsArr) {
                // if code is for Car park set car park layer visible
                if (symbolsArr[symbolCount] == "CPK") {
                    layerState.carparkLayerVisible = true;
                }
                else {
                    // otherwise iterate through the stop layers in the layerstate object and set the layer if the name is same as code
                    for (var i = 0; i < layerState.stops.length; i++) {
                        if (symbolsArr[symbolCount] == "BCX") {
                            try {
                                if (map.getMapProperties().scale <= maxBusSymbolMapScale) {
                                    layerState.stops[i].visible = true;
                                }
                                else {
                                    layerState.stops[i].visible = false;
                                }
                            } catch (err) {
                                alert(err);
                            }

                        }
                        else if (symbolsArr[symbolCount] == layerState.stops[i].name) {
                            layerState.stops[i].visible = true;
                            break;
                        }
                    }

                }
            }

            map.setLayerState(layerState);
        }
        catch (err) {
            alert(err);
        }
    }
}