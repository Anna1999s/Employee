$(document).ready(function () {
    $(function () {
        $("#Name").autocomplete({
            source: function (request, response) {
                $.getJSON("/Employee/GetNames", { term: request.term },
                    response);
            },
            minLength: 2
        });
    });

}); 