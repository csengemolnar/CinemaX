document.getElementById('dateForm').addEventListener('submit', function (event) {
    event.preventDefault(); 

    var movieId = document.getElementById('movieId').value;
    var startDate = document.getElementById('startDate').value;
    var endDate = document.getElementById('endDate').value;

    var url = 'http://localhost:5153/Admin/ViewershipTrends';

    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({ movieId, startDate, endDate })
    })
        .then(response => response.json())
        .then(data => createLineChart(data))
        .catch(error => console.error('Error:', error));

});

function createLineChart(data) {
    console.log(data);
    Highcharts.chart('container', {
        chart: {
            type: 'line'
        },
        title: {
            text: 'Viewership Trend'
        },
        xAxis: {
            type: 'datetime',
            title: {
                text: 'Date'
            }
        },
        yAxis: {
            title: {
                text: 'Number of Viewers'
            }
        },
        series: [{
            name: 'Screenings',
            data: data.map(item => [Date.parse(item.name), item.y])
        }]
    });
}
