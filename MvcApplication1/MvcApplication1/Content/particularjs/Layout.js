

function searchPanel() {
    $("#searchPanel").toggle();
    $("#toggleSearch").click(function () {
        $("#searchPanel").toggle();
    }
    );
}

function searchSubmit() {

    document.getElementById("searchBtn").onclick = function (e) {
        SearchFunc();
        e.preventDefault();
    }
}

function SearchFunc() {

    var name = document.getElementById("Name");
    var location = document.getElementById("Location");
    var priceLow = document.getElementById("PriceLow");

    var available = document.getElementById("availableButton");

    var reg = /^\d*$/;
    var regex = new RegExp(reg);

    var anyFault;

    if (priceLow.value != '' && !regex.test(priceLow.value)) {
        priceLow.style.borderColor = "#EE5555";
        anyFault = "";
    }

    var priceHigh = document.getElementById("PriceHigh");
    if (priceHigh.value != '' && !regex.test(priceHigh.value)) {
        priceHigh.style.borderColor = "#EE5555";
        anyFault = "";
    }

    var capacity = document.getElementById("Capacity");
    if (capacity.value != '' && !regex.test(capacity.value)) {
        capacity.style.borderColor = "#EE5555";
        anyFault = "";
    }

    var availFrom = document.getElementById("AvailFrom");

    var availTo = document.getElementById("AvailTo");

    var fParse;
    var tParse;

    if (anyFault === "" ||
        name.value == "" && location.value == "" && priceLow.value == "" && priceHigh.value == "" && capacity.value == "" && availFrom.value == "" && availTo.value == "") {
        return false;
    }

    var params = name.name + "=" + name.value + "&" + location.name + "=" + location.value + "&" + priceLow.name + "=" + priceLow.value + "&" + priceHigh.name + "=" + priceHigh.value + "&" + capacity.name + "=" + capacity.value + "&" + availFrom.name + "=" + availFrom.value + "&" + availTo.name + "=" + availTo.value;
    window.location = "../../Search?" + params;
    return false;
}