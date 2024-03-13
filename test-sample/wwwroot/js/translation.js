'use strict';
(function(){
  
  $(function(){
    const _ = bindDatatable(); 
  });

  function bindDatatable() {
    return $('#translations-table')
        .DataTable({
            "ajax": "/api/Translations",
            "serverSide": true,
            "processing": true,
            "language": {
              "emptyTable": "No record found.",
            },
            "columns": [
                {
                  "data": "text",
                  "autoWidth": true,
                  "searchable": true
                },
                {
                  "data": "translated",
                  "autoWidth": true,
                  "searchable": true
                },
                {
                  "data": "translator",
                  "autoWidth": true,
                  "searchable": true
                }
            ]
        });
}

})()