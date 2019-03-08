var _currentPageID = 1;
var _pageSize = 10;
var _sortExpression = '';
var _sortDirection = 2;
var _accessUrl = '';
var _gridDivId = 'ajaxGrid';
var _searchBoxDivId = 'searchFilter';
var _searchParameters = [];
var _showAllRecords = false;
var _showInactive = false;

function asynList() {
    var jsonParameters = { "CurrentPageID": _currentPageID, "PageSize": _pageSize, "SortingColumn": _sortExpression, "SortingOrder": _sortDirection, "SearchParameter": _searchParameters, "ShowAllRecord": _showAllRecords, "ShowInactive": _showInactive };
    $.ajax({
        type: 'POST',
        cache: false,
        contentType: 'application/json',
        data: JSON.stringify({ dataPaging: jsonParameters }),
        url: _accessUrl,
        success: function (result) {
            $('#' + _gridDivId).html(result);
            $('.sort-icon').removeClass('sort-asc');
            $('.sort-icon').removeClass('sort-desc');

            $('.sort-icon').each(function (index) {
                if ($(this).attr('onclick') == "sortRequest('" + _sortExpression + "')") {
                    if (_sortDirection == 1) {
                        $(this).addClass('sort-asc');
                    }
                    else {
                        $(this).addClass('sort-desc');
                    }
                }
            });

        },
        error: function (request, status, error) {
            // look for status of 401 and redirect to login
            if (status == 403) {
                window.location = "/";
            }
        },
        //beforeSend: function () {
        //    $('.wait-pupup').fadeIn();
        //},
        //complete: function () {
        //    $('.wait-pupup').fadeOut();
        //}
    });
}

$(document).ready(function () {
    $('body').on('change', '#selectPageSize', function () {
        _pageSize = parseInt(this.value);
        _currentPageID = 1;
        asynList();
    });
});

function pageRequest(pageid) {
    _currentPageID = parseInt(pageid);
    asynList();
}

function searchRequest() {
    _searchParameters.splice(0, _searchParameters.length);
    $('#' + _searchBoxDivId).find('input:text').each(function () {
        if ($(this).val() != '' && $(this).val() != null) {
            var parameter = { Key: $(this).attr('name'), Value: $(this).val() }
            _searchParameters.push(parameter);
        }
    });
    $('#' + _searchBoxDivId).find('input:checkbox').each(function () {
        var parameter = { Key: $(this).attr('name'), Value: $(this).is(':checked') }
        _searchParameters.push(parameter);
    });
    $('#' + _searchBoxDivId).find('select').each(function () {
        if ($(this).val() != '' && $(this).val() != null) {
            var parameter = { Key: $(this).attr('name'), Value: $(this).val() }
            _searchParameters.push(parameter);
        }
    });
    $('#' + _searchBoxDivId).find('input:hidden').each(function () {
        if ($(this).val() != '' && $(this).val() != null) {
            var parameter = { Key: $(this).attr('name'), Value: $(this).val() }
            _searchParameters.push(parameter);
        }
    });
    _currentPageID = 1;
    asynList();
}

function sortRequest(sortColumn) {
    if (_sortExpression == sortColumn) {
        if (_sortDirection == 1) {
            _sortDirection = 2;
        }
        else {
            _sortDirection = 1;
        }
    }
    else {
        _sortDirection = 1;
    }
    _currentPageID = 1;
    _sortExpression = sortColumn;
    asynList();
}