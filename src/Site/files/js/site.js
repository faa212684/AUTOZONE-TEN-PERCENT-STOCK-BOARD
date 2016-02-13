$(function() {
  $('#search').submit(function(eventObject) {

    var query = $('#query').val();

    var list = $('#search-results div.list-group').empty();

    $('#search-results').collapse('show');

    $.getJSON("/api/search/",
      { q: query },
      function(results) {
        if (!results || !results.length) {
          list.append('<p>No results found for: "' + query + '"<p/>');
        }
        else if (results.length === 1) {
          window.location.href = results[0].url;
        }
        else {
          $.each(results, function(index, result) {
            var from = result.source ? ' from file ' + result.source : '';
            list.append('<a class="list-group-item" href="' + result.url + '">' + result.code + ' in area ' + result.area + from + '</a>');
          });
        }
      });

    eventObject.preventDefault();
  });
});