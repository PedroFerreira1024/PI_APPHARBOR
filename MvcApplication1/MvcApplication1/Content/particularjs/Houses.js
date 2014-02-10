
function revPagination()
{
    window.addEventListener('popstate', function (event) {
        reversePaging();
    });
}

function reversePaging()
{
    var xmlhttp;

    xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            document.getElementById("houseList").innerHTML = xmlhttp.responseText;
        }
    }

    xmlhttp.open("GET", location.href, true);
    xmlhttp.send();
    
}

function pagination() {
    
    

    var pageButtons = document.getElementsByClassName("pageButton");
    for (var i = 0; i < pageButtons.length; ++i) {
        (function (idx) {
            document.getElementById("pageButton" + (idx + 1)).onclick = function () {
                (function (index) {
                    $(".pageButton").css("background", "none");
                    $("#pageButton" + index).css("background", "#DDEEFF");
                    ajaxPaging(index);
                })(idx + 1);
            }
        })(i);
    }

    function ajaxPaging(index) {
            var xmlhttp;

            xmlhttp = new XMLHttpRequest();

            xmlhttp.onreadystatechange = function () {
                if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
                    document.getElementById("houseList").innerHTML = xmlhttp.responseText;
                }
            }
            
            var path = location.pathname;
            var query = "page="+index;

            var queryPairs = location.search.split("&");
            
            if (location.search === "" || queryPairs.length == 1) {
                query = "?" + query;
            }
            else {
                if (queryPairs[queryPairs.length - 1].split("=")[0] === "page")
                    query = location.search.slice(0, location.search.length - (queryPairs[queryPairs.length - 1].length + 1)) + "&" + query;
                else
                    query = location.search + "&" + query;
            }
                
            var url = path + query;

            history.pushState("", "", url);

            xmlhttp.open("GET", url, true);
            xmlhttp.send();
        }
}