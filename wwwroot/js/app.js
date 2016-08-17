
var textSearch,
    broadSearch,
    booleanSearch,
    orderBy,
    limit,
    url,
    timer;

// Build the url query string from inputs and send ajax request
function searchFeed() {

    textSearch = $('#textSearch').val();
    console.log('textSearch: ',textSearch);
    broadSearch = $('#broadSearch').val();
    booleanSearch = $('#booleanSearch').val();
    orderBy = $('#orderBy').val();
    limit = $('#limit').val();

    url = '?'

    if (textSearch) {
        url += "textsearch=" + textSearch + "&";
    }
    if (broadSearch) {
        url += "broadsearch=" + broadSearch + "&";
    }
    if (booleanSearch) {
        url += "booleansearch=" + booleanSearch + "&";
    }
    if (orderBy) {
        url += "orderby=" + orderBy + "&";
    }
    if (limit) {
        url += "limit=" + Limit + "&";
    }
    $.ajax({
        type: "GET",
        url: 'api/feed' + url,
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            buildDataTable(data);
        },
        error: function (e) {
            console.log(e);
        }
   });
 }

$( "input" ).keyup(function( event ) {
    clearTimeout(timer);
    timer = setTimeout(function() {
        searchFeed();
    }, 500);
});

function buildDataTable (data) {
    resetDataTable();
    $('.result-count').append("Results: " + data.length);
    for (i in data[0]) {
        $("table thead tr").append("<th>" + i + "</th>");
    }
    data.forEach(function(elem, index, array) {
        $("tbody").append("<tr class='" + index + "'></tr>");
        for (item in elem) {
            $("tbody ." + index).append("<td><div class='table-item'>" + elem[item] + "</div></td>");
        }
    });
}

function resetDataTable () {
    $('.result-count').empty();
    $("table thead tr").empty();
    $("tbody").empty();
}

function refreshFeed () {
    $.ajax({
        type: "GET",
        url: 'api/feed/refresh',
        data: "",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            console.log('data: ',data);
            buildDataTable(data);
        },
        error: function (e) {
            console.log(e);
        }
   });
}