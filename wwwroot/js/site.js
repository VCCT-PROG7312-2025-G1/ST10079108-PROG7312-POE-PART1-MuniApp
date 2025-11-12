document.addEventListener("DOMContentLoaded", function () {
    const ctx = document.getElementById('issuesChart').getContext('2d');

    const categoryDataElement = document.getElementById('categoryData');
    const categoryData = categoryDataElement ? JSON.parse(categoryDataElement.value) : {};

    const labels = Object.keys(categoryData || {});
    const data = Object.values(categoryData || {});

    new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Number of Issues',
                data: data,
                borderWidth: 1,
                backgroundColor: '#48433D', // column color
                borderColor: '#fff',         // white outlines for contrast
            }]
        },
        options: {
            plugins: {
                legend: {
                    labels: {
                        color: '#fff',  // legend text color
                        font: { size: 14 }
                    }
                },
            },
            scales: {
                y: {
                    beginAtZero: true,
                    title: {
                        display: true,
                        text: 'Issue Count',
                        color: '#fff'
                    },
                    ticks: { color: '#fff' },
                    grid: { color: '#3a3a3a' }
                },
                x: {
                    title: {
                        display: true,
                        text: 'Category',
                        color: '#fff'
                    },
                    ticks: { color: '#fff' },
                    grid: { color: '#3a3a3a' }
                }
            },
            layout: {
                padding: 10
            }
        }
    });
});