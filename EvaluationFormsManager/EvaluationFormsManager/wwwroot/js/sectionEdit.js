function close() {
    $('#jsGrid .jsgrid-grid-header .jsgrid-table .jsgrid-insert-row').css("display", "none");
}

$(function () {

    $("#jsGrid").jsGrid({
        width: "100%",

        inserting: false,
        editing: true,
        sorting: true,
        paging: false,

        controller: {
            loadData: function (filter) {
                var data = $.Deferred();

                $.get("/Forms/Section/Criteria", function (response) {
                    data.resolve(JSON.parse(response));
                });

                return data.promise();
            },

            insertItem: function (item) {
                $.post("/Forms/Section/Criteria/Create", { name: item.Name }).done(function () {
                    $('#addCriterion').show();
                    $('#cancelAddCriterion').hide();

                    $("#jsGrid").jsGrid("loadData");
                });
            },

            updateItem: function (item) {
                $.post("/Forms/Section/Criteria/Edit", { index: item.Index - 1, name: item.Name }).done(function () {
                    $("#jsGrid").jsGrid("loadData");
                });
            },

            deleteItem: function (item) {
                $.post("/Forms/Section/Criteria/Delete", { index: item.Index - 1 }).done(function () {
                    $("#jsGrid").jsGrid("loadData");
                });
            }
        },

        noDataContent: "No criteria!",
        autoload: true,

        fields: [
            { name: "Index", type: "number", width: 50, editing: false, align: "center", title: "#" },
            { name: "Name", type: "text", width: 200, validate: "required", title: "Name" },
            { name: "ModifiedBy", type: "text", editing: false, align: "center", title: "Modified By" },
            { name: "ModifiedDate", type: "text", editing: false, align: "center", title: "Last Modified" },
            { name: "Action", type: "control", inserting: false }
        ]
    });

    $('#addCriterion').click(function () {
        $('#jsGrid .jsgrid-grid-header .jsgrid-table .jsgrid-insert-row').css("display", "table-row");

        for (let i = 0; i < 4; i++)
            if (i != 1)
                $('#jsGrid .jsgrid-grid-header .jsgrid-table .jsgrid-insert-row').find('td').eq(i).html("");

        $('#cancelAddCriterion').show();
        $('#addCriterion').hide();
    });

    $('#cancelAddCriterion').click(function () {
        $('#jsGrid .jsgrid-grid-header .jsgrid-table .jsgrid-insert-row').css("display", "none");

        $('#addCriterion').show();
        $('#cancelAddCriterion').hide();
    })
})