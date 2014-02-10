
function otherImagePopUp() {
    $('.popupSelected').click(function () {

        $("#showUp").attr('src', this.src);

        $("#showUp").dialog({
            resizable: false,
            draggable: false,
            width: 450,
            heigth: 450
        });
    });
}

function calendarInit() {

    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();


    var day = 2;
    var month = 1;
    var year = 0;

    var evts = [];
    var dateIntervals = pageModel.availability.occupied;
    
    for (var i = 0; i < dateIntervals.length; ++i) {
        var dateFrom = dateIntervals[i].from.split("T")[0].split("-");
        var dateTo = dateIntervals[i].to.split("T")[0].split("-");
        evts.push({
            title: 'Reservado',
            start: new Date(dateFrom[year], dateFrom[month], dateFrom[day]),
            end: new Date(dateTo[year], dateTo[month], dateTo[day])
        });
    }

    var options = {
        aspectRatio: 3.5,
        eventColor: "#b33",
        events: evts
    };

    $('#calendar').fullCalendar(options);
}

function setPageButtonClick()
{
    $("#addButton").click(function () { addTestimonial() });
            
    for(var i = 1;i<=5;++i)
    {
        (function (idx) {
            var x = "#rateButton"+idx;
            $(x).click(function(){ addRate(idx)});
        })(i);
    }
}

function addRate(number) {
    document.getElementById("rateButton").value = number + "/(5)";
}

function addTestimonial() {
    var xmlhttp = new XMLHttpRequest();

    xmlhttp.onreadystatechange = function () {
        if (xmlhttp.readyState == 4 && xmlhttp.status == 200) {
            var newDiv = document.createElement("div");
            newDiv.className = "list-group-item";
            newDiv.innerHTML = xmlhttp.responseText;
            document.getElementById("testimonialList").appendChild(newDiv);
        }
    }
    //NewTestimonial form parameters
    var desc = document.getElementById("newTestimonial");
    var rate = document.getElementById("rateButton");

    if (desc.value === "")
        return;

    if (rate.value === "") {
        //Turns red as warning
        setInterval(function () {
            rate.style.background = '#EE5555';
        }, 600);
        setInterval(function () {
            rate.style.background = '#EEEEEE';
        }, 1200);
        return;
    }

    var params = desc.name + "=" + desc.value + "&" + "stars" + "=" + rate.value;
    xmlhttp.open("POST", "../../Testimonial/" + pageModel.nameShort, true);
    xmlhttp.setRequestHeader("Content-type", "application/x-www-form-urlencoded");
    xmlhttp.send(params);
}