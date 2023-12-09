document.getElementById('dateForm').addEventListener('submit', function (event) {
    event.preventDefault(); 

    var startDate = document.getElementById('startDate').value;
    var endDate = document.getElementById('endDate').value;
    
    var url = 'http://localhost:5153/Admin/ScreeningStatistics'; 

    fetch(url, {
        method: 'POST', 
        headers: {
            'Content-Type': 'application/json' 
        },
        body: JSON.stringify({ startDate, endDate }) 
    })
        .then(response => response.json())
        .then(data => createChart(data))
        .catch(error => console.error('Error:', error));
});

function createChart(data) {
    Highcharts.chart('container', {
        chart: {
            type: 'pie'
        },
        title: {
            text: 'Screenings Count'
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        accessibility: {
            point: {
                valueSuffix: '%'
            }
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %'
                },
                showInLegend: true
            }
        },
        series: [{
            name: 'Screenings',
            colorByPoint: true,
            data: data.map(item => ({
                name: item.name,
                y: item.y
            }))
        }]
    });
}
