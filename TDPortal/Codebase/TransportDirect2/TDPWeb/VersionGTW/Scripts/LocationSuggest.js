// Implementing dictionary
function dictionary() {
    this.length = 0;
    this.items = new Array();
    
    for (var i = 0; i < arguments.length; i += 2) {
        if (typeof (arguments[i + 1]) != 'undefined') {
            this.items[arguments[i]] = arguments[i + 1];
            this.length++;
        }
    }

    this.removeItem = function (in_key) {
        var tmp_previous;
        if (typeof (this.items[in_key]) != 'undefined') {
            this.length--;
            var tmp_previous = this.items[in_key];
            delete this.items[in_key];
        }

        return tmp_previous;
    }

    this.getItem = function (in_key) {
        return this.items[in_key];
    }

    this.setItem = function (in_key, in_value) {
        var tmp_previous;
        if (typeof (in_value) != 'undefined') {
            if (typeof (this.items[in_key]) == 'undefined') {
                this.length++;
            }
            else {
                tmp_previous = this.items[in_key];
            }

            this.items[in_key] = in_value;
        }

        return tmp_previous;
    }

    this.hasItem = function (in_key) {
        return typeof (this.items[in_key]) != 'undefined';
    }

    this.clear = function () {
        for (var i in this.items) {
            delete this.items[i];
        }

        this.length = 0;
    }
}

/* Initialising global data stores - so we can use same data store for multiple location dropdowns */
var dataStores = new dictionary();
var downloadQueue = new Array();

/* Initialise a global function which will be called when location javascript file is loaded */
function registerLocationData(dataStoreId, data) {

    // Check if data already loaded in to the store
    if (data != null && $.isArray(data)) {
        if (data.length > 0) {
            if (dataStores.hasItem(dataStoreId)) {
                if (dataStores.getItem(dataStoreId).hasItem(data[0][0].charAt(0).toUpperCase()))
                    return;
            }
        }
    }

    // Create data store and add data
    if (data.length > 0) {

        if (!dataStores.hasItem(dataStoreId)) {
            dataStores.setItem(dataStoreId, new dictionary());
        }

        var dataStore = dataStores.getItem(dataStoreId);

        dataStore.setItem(data[0][0].charAt(0).toUpperCase(), data.sort(function (a, b) {

            // Sort locations on name (or alias name if exists for location), ignoring case
            c = a.length > 3 ? a[3].toUpperCase() : a[0].toUpperCase();
            d = b.length > 3 ? b[3].toUpperCase() : b[0].toUpperCase();

            return (c < d) ? -1 : (c > d) ? 1 : 0;
        }));
    }

}


(function ($, window, undefined) {
    // Utility functions
    $.fn.borderWidth = function () { return $(this).outerWidth() - $(this).innerWidth(); };
    $.fn.paddingWidth = function () { return $(this).innerWidth() - $(this).width(); };
    $.fn.extraWidth = function () { return $(this).outerWidth(true) - $(this).width(); };
    $.fn.offsetFrom = function (e) {
        var $e = $(e);
        return {
            left: $(this).offset().left - $e.offset().left,
            top: $(this).offset().top - $e.offset().top
        };
    };
})(jQuery);

(function ($) {
    // Location suggest plugin
    $.fn.locationSuggest = function (options) {

        var settings = {
            minChars: 3,
            maxResults: 20,
            restrictResults: true,
            scrollable: false,
            scrollableVisibleResultCount: 6,
            minCharsToInitiateGetData: 1,
            prevText: '&nbsp;',
            nextText: '&nbsp;',
            label: null
        };

        var opts = $.extend(settings, options);


        return this.each(function (x) {
            var input = $(this);
            var dataStoreId = getJSHdnSettingFieldValue('scriptId', input.parent().parent().parent());
            var selectedItem = null;
            var mouseDrag = false;
            var timeout = null;
            var suppressKeyPress = false;
            var active_ul = null;
            var closethis = true;
            var initSearch = false;
            var lastMenuCount = 0;

            // this is to stop JAWs from moving to next element and not moving in to the drop down
            $(input).wrap('<span role="application"></span>');

            input.attr({
                "autocomplete": "off",
				role: "textbox",
				"aria-autocomplete": "list",
				"aria-haspopup": "true"
			}).attr("aria-labelledby", opts.label ? $(opts.label).attr("id") :  input.attr('id') + '_Discription');

            x = x + "" + Math.floor(Math.random() * 100);
            var x_id = "locationSuggest" + x;

            var sclabel = $('<div class="screenReaderOnly" id="status-menu" aria-live="polite"></div>');

            var results_container = $('<ul id="' + x_id + '" class="locationSuggestList" role="menu" ></ul>').css("position","absolute").css("width",input.outerWidth() - 2).hide();
            // Get the position offset from the input itself
            var pos = $(input).offset();
            results_container.css("top", pos.top + input.outerHeight() + "px").css("left", pos.left + "px");
            results_container.css("z-index","1").css("background","#ffffff");
            // aria- styling
            results_container.attr("role", "listbox").attr("aria-activedescendant","active-menuitem");
            results_container.attr("aria-live","off");
           
            // Add to the main div rather than next to the target input element
            // this is to resolve the issues with z-index on ie 6/7
            $('.sjpContent').append(results_container);
            //results_container.before(sclabel);
            $('.sjpContent').append(sclabel);
            active_ul = results_container;


            // ------------------
            // EVENTs
            input.bind("focus.locationSuggest",function(){
                
            });
            input.bind("blur.locationSuggest",function (event) {
                
                if (timeout) { clearTimeout(timeout); }
                if (!mouseDrag) {
                    if(closethis)
                        this.closing = setTimeout(hideOnBlur, 200);
                }
                else {
                    mouseDrag = false;
                }
                results_container.attr("aria-live","off");
                closethis = true;
                event.preventDefault();
              
            });
            input.bind("keydown.locationSuggest",function (e) {
                suppressKeyPress = false;
                
                 $('#' + input.attr('id') + '_hdnValue').attr('value','');
                 $('#' + input.attr('id') + '_hdnType').attr('value','');    
                switch (e.which) {
                    case 13: //enter
                        suppressKeyPress = true;
                    case 9: //tab
                        if(active_ul) {
                            var active = $("li.locationSuggestSelectedItem:first", active_ul);
                            if(!active && active.length <= 0) {
                                active = $('li.locationSuggestListItem:first',active_ul);
                            }
                            data = active.data('data');
                            if(data != undefined && data)
                                selectedItem = data.data_item;
                            
                        }
                        hide();
                        break;

                    case 27: // esc
                        selectedItem = null;
                        hide();
                        break;

                    case 33: //page up    
                    case 34: //page down    
                   
                    case 18: //alt    
                        suppressKeyPress = true;
                        break;

                    case 38: //up arrow
                        results_container.attr("aria-live","off");
                        moveSelection("up");
                        results_container.attr("aria-live","polite");
                        e.preventDefault();
                        break;
                    case 40: //down arrow
                        results_container.attr("aria-live","off");
                        moveSelection("down");
                        results_container.attr("aria-live","polite");
                        e.preventDefault();
                        break;
                    default:
                        initSearch = true;
                        break;
                    

                }
            });
            input.bind("keypress.locationSuggest",function (e) {

                if ( suppressKeyPress ) {
					suppressKeyPress = false;
					e.preventDefault();
				} 
                              
            });
            input.bind("keyup.locationSuggest",function(e){
                    if(initSearch) {
                        //hide();
                        if (timeout) { clearTimeout(timeout); }
                                               
                        if($.trim(input.val()).length >= opts.minCharsToInitiateGetData) {
                           
                            if ((!dataStores.hasItem(dataStoreId))
                                || (!dataStores.getItem(dataStoreId).hasItem($.trim(input.val()).charAt(0).toUpperCase())))
                                downloadData();
                            else if($.trim(input.val()).length > 2)
                                timeout = setTimeout(function () { setupList(); }, 200);
                            else
                                hide();
                        }
                        
                        initSearch = false;  
                    } 
            });
            // END EVENTS
            // ------------------

            // ------------------           
            // PRIVATE FUNCTIONS

            // Function initiating download of the location file
            function downloadData() {
                var scriptPath = getJSHdnSettingFieldValue('scriptPath',input.parent().parent().parent());
                var scriptId = getJSHdnSettingFieldValue('scriptId',input.parent().parent().parent());
                var dataAvail = ((dataStores.hasItem(dataStoreId)) && (dataStores.getItem(dataStoreId).hasItem($.trim(input.val()).charAt(0).toUpperCase())));

                dataPartLoad = $.trim(input.val()).charAt(0).toUpperCase();
                if(dataPartLoad.length > 0) {
                    if (!dataAvail && $.inArray(dataStoreId + dataPartLoad, downloadQueue) < 0) {
                        if (scriptPath && scriptId) {
                            datascript = scriptPath + scriptId +"_" + dataPartLoad + ".js";
                            downloadQueue.push(dataStoreId + dataPartLoad);
                            $.getScript(datascript, function() {
                           
                            });
                        }
                    }
                }
            }

           // Sets up the dropdown list ready to show
            function setupList() {
                  
                   var listItems = getListItems();
                   results_container.html("");
                   
                   if(listItems.length > 0) {
                       var itemCount = 0, pageCount = 0;
                       $.each(listItems,function(intex, item){
                            itemCount++;

                   	        var cssClass = "locationSuggestListItem";
                            if (itemCount == listItems.length)
                                cssClass = "locationSuggestListItem locationSuggestListItemLast";

                            var formatted = $('<li class="' + cssClass + '" role="menuitem"></li>')
                                    .width(active_ul.outerWidth())
                                    .mouseover(function(){
                                         $("li", active_ul).removeClass("locationSuggestSelectedItem").find('a').each(function(elem){ $(this).removeAttr('id');});
								         $(this).addClass("locationSuggestSelectedItem").find('a').each(function(elem){ $(this).attr('id','active-menuitem');});
                                                 
                                    })
                                    .mousedown(function(){ input_focus = false; })
                                    .data("data",{data_item: item}).html('<a tabindex="-1">'+ ((item.length> 3 && item[3].length) > 0 ? item[3] : item[0]) +'</a>');
                            $(formatted).find('a').unbind('click');
                            $(formatted).find('a').live('click', function(event){
                                            
                                var raw_data = $(this).parent().data("data");
							                
		                        var data_item = raw_data.data_item;
									        
                                selectedItem = data_item;
                                hide();
                            }).mouseover(function(){
			                                    results_container.attr("aria-live","off");
						                        results_container.attr("aria-live","polite");
                                                  $(this).html($(this).html()).unbind();
                                                  $(this).bind('click',function(event){
                                                    var raw_data = $(this).parent().data("data");
							                
		                                            var data_item = raw_data.data_item;
									        
                                                    selectedItem = data_item;
                                                    hide(true);
                                                  });
                                                 
								        });
                            results_container.append(formatted);
                            if(opts.restrictResults && itemCount >= opts.maxResults ) {
                                return false;
                            }
                       });
                       show();
                       
                   }
                   else {
                       hide();
                       statusMessage ("No matches");
                   }
            }

            // Search and find the location items to show in the drop down
            function getListItems() {
                 var listToSearch = dataStores.getItem(dataStoreId).getItem($.trim(input.val()).charAt(0).toUpperCase());

                 var result = new Array();
                 var search = escapeRegex($.trim(input.val().toUpperCase()));
                 if(listToSearch.length > 0) {
                     var stIndex = getStartIndex(listToSearch);
                     var C = /*$.trim(input.val().toUpperCase())*/ search + "~";

                     for (var L = stIndex; (listToSearch[L].length > 3  && listToSearch[L][3].length > 0 ? listToSearch[L][3] : listToSearch[L][0]).toUpperCase() < C; L++) {
                       
                        searchString = $.trim(input.val().toUpperCase());
                        matchString = $.trim((listToSearch[L].length > 3  && listToSearch[L][3].length > 0 ? listToSearch[L][3] : listToSearch[L][0]).toUpperCase()).substring(0, searchString.length);

                        searchStringWithHyphen = $.trim(input.val().toUpperCase()).replace(/\s/g, "-");
                        matchStringWithHyphen = $.trim((listToSearch[L].length > 3  && listToSearch[L][3].length > 0 ? listToSearch[L][3] : listToSearch[L][0]).toUpperCase()).substring(0, searchString.length);

                        if (matchString == searchString || searchStringWithHyphen == matchStringWithHyphen) {
                            stopObj = listToSearch[L];
                            result.push(stopObj);
                        }
                        

                        if (L == listToSearch.length - 1) break;
                    }

                 }

                 return result;
            }

            function escapeRegex( value ) {
		        return value.replace(/[-[\]{}()*+?.,'\\^$|#\s]/g, "\\$&");
	        }

            // Implements the algorithm to find the nearest matching item index to start looking in the list
            function getStartIndex(data){
                var I = ($.trim(input.val().replace(/-'/g, ""))).toUpperCase();
    
                var E = 0;

                var A = data.length - 1;

                if (data.length > 0) {
                    while (E <= A) {

                        var G = Math.floor((E + A) / 2);

                        var B = (data[G].length > 3  && data[G][3].length > 0 ? data[G][3] : data[G][0].replace(/'/g, "")).toUpperCase();
                                               
                        if (B < I) {
                            E = G + 1;
                        } else {
                            if (B > I) {
                                A = G - 1;
                            } else {
                   
                                return G;
                            }
                        }
                    }
                }

                return E > A ? (A < 0 ? 0 : A) : E;
            }

            function show() {
                results_container.slideDown(0);
                results_container.attr("aria-live","off");
                announceItemCount (results_container); 
                input.focus();
            }

            function menuVisible ($menu) {
                if ($menu.css("display") != "none")
                    return true;
                return false;
            } // menuVisible


            function announceItemCount ($menu) {
                var menuCount = $menu.children().length;
                if (! menuVisible ($menu) ) statusMessage ("No matches");
                else {
                    setTimeout (function () {
                        statusMessage (menuCount + " items in the list.");
                    }, 0); // setTimeout
                }

                lastMenuCount = menuCount;
            } // announceMatchCount

            function statusMessage (message) {

	            if (message)
	            $("#status-menu").html (message + "<br />\n");

	        } // statusMessage

            function hide(moveToNext) {
                if(selectedItem != undefined && selectedItem != null) {
                
                    input.val(selectedItem[0]).focus();
									
                    $('#' + input.attr('id') + '_hdnValue').attr('value',selectedItem[1]);
                    $('#' + input.attr('id') + '_hdnType').attr('value',selectedItem[2]);                                    	   
                    selectedItem = null;
                    statusMessage ("Selected" + input.val());

                    if (moveToNext == true){
                        // Find next non-location-input element and focus
                        $(input).parent().parent().parent().find('input:not(.locationInput):not(.locationClearButton):not(:hidden)').focus();
                    }
                 }

                 results_container.fadeOut(0);                
            }

            function hideOnBlur() {
                if (this.closing) { clearTimeout(this.closing); }
                results_container.html("");
                selectedItem = null;
                hide();
            }

            // Keyboard navigation of the items
            function moveSelection(direction) {
                if(!active_ul) {
                    active_ul = $("ul.locationSuggestList:visible", results_container);
                }
                                
                if ($(":visible", active_ul).length > 0) {
                    var lis = $("li", active_ul);
                    if (direction == "down") {
                        var start = lis.eq(0);
                    } else {
                        var start = lis.filter(":last");
                    }
                    var active = $("li.locationSuggestSelectedItem:first", active_ul);
                    if (active.length > 0) {
                        if (direction == "down") {
                            start = active.next();
                        } else {
                            start = active.prev();
                        }
                    }
                    lis.removeClass("locationSuggestSelectedItem").blur().find('a').each(function(elem){ $(this).removeAttr('id');});
                    start.addClass("locationSuggestSelectedItem").focus().find('a').each(function(elem){ $(this).attr('id','active-menuitem');});
                    results_container.attr("aria-live","polite");
                    //$(start).html($(start).html());
                    
                    // Rebind click as it is lost when navigating with keyboard
                    $(start).html($(start).html()).unbind();
                    $(start).bind('click', function(event) {
                        var raw_data = $(start).data("data");

                        var data_item = raw_data.data_item;

                        selectedItem = data_item;
                        hide();
                    });

                    // set selected item data (null check in case no location row is selected
                    var raw_data = $(start).data("data");
                    if (raw_data) {
                        var data_item = raw_data.data_item;

                        selectedItem = data_item;
                    }

                    // Not sure why this is done for IE, but commented out until reason established
//                    if ($.browser.msie) {
//                        if ($("a", start).html()) {

//                            input.val($("a", start).html());
//                        }
//                    }
                }
            }

            // END PRIVATE FUNCTIONS
            // ------------------
           
        }); // END this.each
        
    }; // END fn.locationSuggest

})(jQuery);         // END function($)


/* Jquery page ready method - runs soon after page loaded */
$(document).ready(function () {

    $('.locationBox').locationSuggest();

    // Following lines are to rebind the location suggest functionality after ajax update of the update panel
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_endRequest(function () {
        $('.locationBox').locationSuggest();
    });


});