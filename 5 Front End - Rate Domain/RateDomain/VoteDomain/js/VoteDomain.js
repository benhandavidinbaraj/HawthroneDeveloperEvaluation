$(document).ready(function () {
    //Read domain from Domain.json file and create table
    $.getJSON("Asset/Domain.json", function (data) {
        var items = [];
        items.push("<table id='TableDomain'><thead><tr><th>Domain</th><th>Vote</th><th>Number of votes</th></tr></thead> <tbody>");
        $.each(data, function (key, val) {
            items.push(" <tr class='Data_Row'><td data-title='Domain'>" + val + "</td ><td data-title='Vote'><a class='RemoveVotebutton' onclick='RemoveVote(this)'>&nbsp;- &nbsp;</a><a  class='AddVoteButton' onclick='AddVote(this)'>&nbsp;+&nbsp;</a></td><td class='bottomBorder' data-title='Number of votes'>0</td> </tr>");
        });
        items.push("</tbody></table>");
        $("body").append(items.join(""));
    });
});

//Move Sort button on page scroll
$(window).scroll(function () {
    var winScrollTop = $(window).scrollTop();
    var winHeight = $(window).height();
    var top = winScrollTop + 40 ;
    $('#Scrollbutton').css({ 'top': top + 'px' });
});

//function to remove a vote
function RemoveVote(e) {
    var row, cell, cellContent
    row = $(e).closest('tr');
    cell = $(row).find("td:eq('2')");
    cellContent = $(cell).html();
    $(cell).html(parseInt(cellContent) - 1);
    isSorted = false;
}

//function to add vote
function AddVote(e) {
    var row, cell, cellContent
    row = $(e).closest('tr');
    cell = $(row).find("td:eq('2')");
    cellContent = $(cell).html();
    $(cell).html(parseInt(cellContent) + 1);
    isSorted = false;
}

var isSorted = true;//Variable to validate page is already sorted

//function to sort table based on number of votes in descening order
function SortTable() {
    if (isSorted == true)
        return;
    var tableName = "TableDomain";
    var rowClass = "Data_Row";
    var columnNumber = 2;
    var ascending = false;
    var row, cell, cellContent;
    var comparisonRow, comparisonCell, comparisonContent;

    $("#" + tableName + ">tbody tr." + rowClass).each(function (i) {
        row = $("#" + tableName + ">tbody tr." + rowClass + ":eq(" + i + ")");
        cell = $(row).find("td:eq(" + columnNumber + ")");
        cellContent = parseInt($(cell).html());

        $("#" + tableName + ">tbody tr." + rowClass).each(function (j) {
            comparisonRow = $("#" + tableName + ">tbody tr." + rowClass + ":eq(" + j + ")");
            comparisonCell = $(comparisonRow).find("td:eq(" + columnNumber + ")");
            comparisonContent = parseInt($(comparisonCell).html());

            if ((ascending && cellContent < comparisonContent) || (!ascending && cellContent > comparisonContent)) {
                $(row).insertBefore(comparisonRow);
                return false;
            }
        });
    });
    isSorted = true;
}