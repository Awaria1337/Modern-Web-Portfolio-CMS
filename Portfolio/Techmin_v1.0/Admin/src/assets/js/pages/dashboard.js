/**
 * Theme: Techmin - Responsive Bootstrap 5 Admin Dashboard
 * Author: Techzaa
 * Module/App: Dashboard
 */

!(function ($) {
    "use strict";

    var Dashboard = function () {
        (this.$body = $("body")), (this.charts = []);
    };

    (Dashboard.prototype.initCharts = function () {
        window.Apex = {
            chart: {
                parentHeightOffset: 0,
                toolbar: {
                    show: false,
                },
            },
            grid: {
                padding: {
                    left: 0,
                    right: 0,
                },
            },
            colors: ["#3e60d5", "#47ad77", "#fa5c7c", "#ffbc00"],
        };

        // --------------------------------------------------
        var colors = ["#3e60d5", "#47ad77", "#fa5c7c", "#ffbc00"];

        //
        // reavenue chart
        //
        var dataColors = $("#revenue-report").data("colors");
        if (dataColors) {
            colors = dataColors.split(",");
        }
        var options = {
            chart: {
                type: "area",
                height: 360,
                toolbar: {
                    show: !1,
                },
            },
            series: [
                {
                    name: "Day",
                    data: [40, 60, 44, 84, 64, 110, 95],
                },
                {
                    name: "Week",
                    data: [20, 30, 22, 42, 32, 55, 44],
                },
            ],
            stroke: {
                curve: "straight",
                width: ["4", "4"],
            },
            grid: {
                xaxis: {
                    lines: {
                        show: !0,
                    },
                },
                yaxis: {
                    lines: {
                        show: !0,
                    },
                },
            },
            colors: colors,
            xaxis: {
                categories: [
                    "Jan",
                    "Feb",
                    "Mar",
                    "Apr",
                    "May",
                    "Jun",
                    "Jul",
                    "Aug",
                    "Sep",
                    "Oct",
                    "Now",
                    "Des",
                ],
            },
            legend: {
                show: !1,
            },
            fill: {
                type: "gradient",
                gradient: {
                    shadeIntensity: 1,
                    inverseColors: !1,
                    opacityFrom: 0.4,
                    opacityTo: 0.1,
                    stops: [30, 100, 100, 100],
                },
            },
            dataLabels: {
                enabled: !1,
            },
        };

        var chart = new ApexCharts(
            document.querySelector("#revenue-report"),
            options
        );
        chart.render();

        //
        // daily orders
        //
        var dataColors = $("#daily-orders").data("colors");
        if (dataColors) {
            colors = dataColors.split(",");
        }
        var options = {
            chart: {
                type: "bar",
                height: 80,
                sparkline: {
                    enabled: !0
                }
            },
            plotOptions: {
                bar: {
                    columnWidth: "80%"
                }
            },
            colors: colors = dataColors ? dataColors.split(",") : colors,
            series: [{
                data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
            }],
            labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
            xaxis: {
                crosshairs: {
                    width: 1
                }
            },
            tooltip: {
                fixed: {
                    enabled: !1
                },
            }
        };

        var chart = new ApexCharts(
            document.querySelector("#daily-orders"),
            options
        );
        chart.render();

        //
        // reavenue chart
        //
        var dataColors = $("#new-leads-chart").data("colors");
        if (dataColors) {
            colors = dataColors.split(",");
        }
        var options = {
            chart: {
                type: "line",
                height: 80,
                sparkline: {
                    enabled: !0
                }
            },
            series: [{
                data: [25, 66, 41, 89, 63, 25, 44, 12, 36, 9, 54]
            }],
            stroke: {
                width: 2,
                curve: "straight",
            },
            markers: {
                size: 0
            },
            colors: colors = dataColors ? dataColors.split(",") : colors,
            tooltip: {
                fixed: {
                    enabled: !1
                },
            }
        };

        var chart = new ApexCharts(
            document.querySelector("#new-leads-chart"),
            options
        );
        chart.render();

        //
        // Booked
        //
        var dataColors = $("#booked-revenue-chart").data("colors");
        if (dataColors) {
            colors = dataColors.split(",");
        }
        var options = {
            chart: {
                type: "bar",
                height: 80,
                sparkline: {
                    enabled: !0
                }
            },
            plotOptions: {
                bar: {
                    columnWidth: "60%"
                }
            },
            colors: colors = dataColors ? dataColors.split(",") : colors,
            series: [{
                data: [47, 45, 74, 14, 56, 74, 14, 11, 7, 39, 82]
            }],
            labels: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11],
            xaxis: {
                crosshairs: {
                    width: 1
                }
            },
            tooltip: {
                fixed: {
                    enabled: !1
                },
            }
        };

        var chart = new ApexCharts(
            document.querySelector("#booked-revenue-chart"),
            options
        );
        chart.render();
    }),
        //initializing various components and plugins
        (Dashboard.prototype.init = function () {
            var $this = this;

            // init charts
            this.initCharts();

            //
            // World map
            //
            $("#world-map-markers").vectorMap({
                map: "world_mill_en",
                normalizeFunction: "polynomial",
                hoverOpacity: 0.7,
                hoverColor: false,
                regionStyle: {
                    initial: {
                        fill: "rgba(145,166,189,.25)",
                    },
                },
                markerStyle: {
                    initial: {
                        r: 9,
                        fill: "#3e60d5",
                        "fill-opacity": 0.9,
                        stroke: "#fff",
                        "stroke-width": 7,
                        "stroke-opacity": 0.4,
                    },

                    hover: {
                        stroke: "#fff",
                        "fill-opacity": 1,
                        "stroke-width": 1.5,
                    },
                },
                backgroundColor: "transparent",
                markers: [
                    {
                        latLng: [41.9, 12.45],
                        name: "Vatican City",
                    },
                    {
                        latLng: [43.73, 7.41],
                        name: "Monaco",
                    },
                    {
                        latLng: [-0.52, 166.93],
                        name: "Nauru",
                    },
                    {
                        latLng: [-8.51, 179.21],
                        name: "Tuvalu",
                    },
                    {
                        latLng: [43.93, 12.46],
                        name: "San Marino",
                    },
                    {
                        latLng: [47.14, 9.52],
                        name: "Liechtenstein",
                    },
                    {
                        latLng: [7.11, 171.06],
                        name: "Marshall Islands",
                    },
                    {
                        latLng: [17.3, -62.73],
                        name: "Saint Kitts and Nevis",
                    },
                    {
                        latLng: [3.2, 73.22],
                        name: "Maldives",
                    },
                    {
                        latLng: [35.88, 14.5],
                        name: "Malta",
                    },
                    {
                        latLng: [12.05, -61.75],
                        name: "Grenada",
                    },
                    {
                        latLng: [13.16, -61.23],
                        name: "Saint Vincent and the Grenadines",
                    },
                    {
                        latLng: [13.16, -59.55],
                        name: "Barbados",
                    },
                    {
                        latLng: [17.11, -61.85],
                        name: "Antigua and Barbuda",
                    },
                    {
                        latLng: [-4.61, 55.45],
                        name: "Seychelles",
                    },
                    {
                        latLng: [7.35, 134.46],
                        name: "Palau",
                    },
                    {
                        latLng: [42.5, 1.51],
                        name: "Andorra",
                    },
                    {
                        latLng: [14.01, -60.98],
                        name: "Saint Lucia",
                    },
                    {
                        latLng: [6.91, 158.18],
                        name: "Federated States of Micronesia",
                    },
                    {
                        latLng: [1.3, 103.8],
                        name: "Singapore",
                    },
                    {
                        latLng: [0.33, 6.73],
                        name: "SÃ£o TomÃ© and PrÃ­ncipe",
                    },
                ],
            });
        }),
        //init flotchart
        ($.Dashboard = new Dashboard()),
        ($.Dashboard.Constructor = Dashboard);
})(window.jQuery),
    //initializing Dashboard
    (function ($) {
        "use strict";
        $(document).ready(function (e) {
            $.Dashboard.init();
        });
    })(window.jQuery);