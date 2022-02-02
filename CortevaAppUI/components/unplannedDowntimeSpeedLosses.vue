<template>
    <div class="d-flex main-container">
        <!-- Interval, site and production line selection menu -->
        <div class="d-flex selection-menu">
            <!-- Site and production line selection-->
            <div class="d-flex site-pl-selection">
                    <label for="site-selection">Site: </label>
                    <select id="site-selection" v-model="site">
                        <option disabled selected value>-- {{$t("select")}} --</option>
                        <template v-for="site of sites[0]">
                            <option v-bind:key="site.name" v-bind:value="site.name">{{site.name}}</option>
                        </template>
                    </select>

                    <label for="pl-selection">{{$t("productionLine")}}: </label>
                    <select id="pl-selection" v-on:change="productionLineSelected();">
                        <option disabled selected value>-- {{$t("select")}} --</option>
                        <template v-for="productionLine of sites[1]">
                            <template v-if="productionLine.name === site">
                                <option v-bind:key="productionLine.productionline_name" v-bind:value="productionLine.productionline_name">
                                    {{productionLine.productionline_name}}
                                </option>
                            </template>
                        </template>
                    </select>

                    <button id="pl-selection-load" type="button" class="btn btn-primary" v-on:click="chargeData();">{{$t("load")}}</button>
            </div>

            <div class="production-window-container">
                <production-window />
            </div>
        </div>


        <div class="content-panel">
            <span class="content-title">{{$t("speedLosses")}}</span>
                <div class="table-panel no-bottom-border rounded-top-left">
                    <span class="content-subtitle">{{$t("fillerOwnStop")}}</span>
                    <div class="table-container">
                        <table class="table table-responsive table-hover">
                            <thead>
                                <tr>
                                    <th scope="col">{{$t("productionOrder")}}</th>
                                    <th scope="col">{{$t("format")}}</th>
                                    <th scope="col">{{$t("designRate")}}</th>
                                    <th scope="col">{{$t("reduceRate")}}</th>
                                    <th scope="col">{{$t("wastedTime")}}</th>
                                    <th scope="col">{{$t("comments/reason")}}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="event of slEventsByTable.fillerStop">
                                        <tr :key="event">
                                            <td>{{event.id}}</td>
                                            <td>{{event.size}} L</td>
                                            <td>{{event.idealRate}}</td>
                                            <td>{{event.reducedRate}}</td>
                                            <td>{{event.duration}}</td>
                                            <td>{{event.comment}}</td>
                                        </tr>
                                </template>
                            </tbody>
                        </table>
                    </div>
                    
                </div>
                <div class="chart-panel no-bottom-border rounded-top-right">
                    <div class="chart-container">
                        <canvas class="chart" id="filler-stop-sl-chart"></canvas>
                    </div>
                    
                </div>

                <div class="table-panel rounded-bottom-left">
                    <span class="content-subtitle">{{$t("reduceRateAtFiller")}}</span>
                    <div class="table-container">
                        <table class="table table-responsive table-hover">
                            <thead>
                                <tr>
                                    <th scope="col">{{$t("productionOrder")}}</th>
                                    <th scope="col">{{$t("format")}}</th>
                                    <th scope="col">{{$t("designRate")}}</th>
                                    <th scope="col">{{$t("reduceRate")}}</th>
                                    <th scope="col">{{$t("wastedTime")}}</th>
                                    <th scope="col">{{$t("comments/reason")}}</th>
                                </tr>
                            </thead>
                            <tbody>
                                <template v-for="event of slEventsByTable.reducedRate">
                                    <tr :key="event">
                                        <td>{{event.id}}</td>
                                        <td>{{event.size}} L</td>
                                        <td>{{event.idealRate}}</td>
                                        <td>{{event.reducedRate}}</td>
                                        <td>{{event.duration}}</td>
                                        <td>{{event.comment}}</td>
                                    </tr>
                                </template>
                            </tbody>
                        </table>
                    </div>
                    
                </div>
                <div class="chart-panel rounded-bottom-right">
                    <div class="chart-container">
                        <canvas class="chart" id="reduced-rate-sl-chart"></canvas>
                    </div>
                </div>
            </div>
    </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import ProductionWindow from './productionWindow.vue';

    export default {
        name: "unplannedDowntimeSpeedLosses",

        data() {

            var data = {

                currentYear: (new Date()).getFullYear(),
                site: '',
                productionLine: '',
                chartObjects: {
                    'filler-stop': {
                        chart: undefined
                    },
                    'reduced-rate': {
                        chart: undefined
                    },
                    created: false
                },
                slEvents: {
                    'Reduced Rate At Filler': {events: [], totalDuration: 0, percentage: 0},
                    'Reduced Rate At An Other Machine': {events: [], totalDuration: 0, percentage: 0},
                    'Filler Own Stoppage': {events: [], totalDuration: 0, percentage: 0},
                    'Filler Stop By Other Machine': {events: [], totalDuration: 0, percentage: 0}
                },
                dbNames: {
                    fillerStop: ['Filler Own Stoppage', 'Filler Stop By Other Machine'],
                    reducedRate: ['Reduced Rate At Filler', 'Reduced Rate At An Other Machine']
                },
                slEventsByTable: {
                    fillerStop: [],
                    reducedRate: []
                }
            }

            return data;
        },

        methods: {
            resolveAfter: function(milliseconds) {
                return new Promise(resolve => {
                    setTimeout(() => resolve(), milliseconds);
                });
            },

            productionLineSelected: function() {
                if (document.getElementById("pl-selection").value) {

                }

            },

            createDowntimeObject: function() {

            },

            chargeData: function() {
                const site = document.getElementById('site-selection').value;
                const selectedPL = document.getElementById('pl-selection').value;
                const begDate = document.getElementById('select-date-from').value;
                const endDate = document.getElementById('select-date-to').value;

                if (site && selectedPL && begDate && endDate) {
                    const params = [site, selectedPL, begDate, endDate];
                
                    this.$store.dispatch('getSpeedLosses', params).then(() => {

                        this.resolveAfter(1000).then(() => {

                            if (!this.chartObjects.created) this.createCharts();

                            //Reinitialize slEvents as empty arrays
                            Object.keys(this.slEvents).forEach(key => this.slEvents[key].events = []);

                            let chartData = {
                                "Filler Own Stoppage": {duration: 0, freq: 0},
                                "Filler Stop By Other Machine": {duration: 0, freq: 0},
                                "Reduced Rate At Filler": {duration: 0, freq: 0},
                                "Reduced Rate At An Other Machine": {duration: 0, freq: 0}
                            };

                            //Add fetched events to the slEvents variable
                            //Creates charts' data
                            if (this.getSpeedLosses.SLEVENTS) {
                                this.slEvents = this.getSpeedLosses.SLEVENTS.reduce((acc, slEvent) => {
                                    if (acc[slEvent.reason]) {
                                        //If event is concerned by a chart, create its data
                                        if (chartData[slEvent.reason]) {
                                            chartData[slEvent.reason].duration += slEvent.duration;
                                            chartData[slEvent.reason].freq++;
                                        }

                                        acc[slEvent.reason].totalDuration += slEvent.duration;
                                        const reducedRate = Math.floor(slEvent.qtyProduced / slEvent.workingDuration);
                                        slEvent.reducedRate = reducedRate || 0;
                                        acc[slEvent.reason].events.push(slEvent);
                                    }

                                    return acc;
                                }, this.slEvents);

                                //Join table events
                                this.slEventsByTable.reducedRate = [];
                                this.slEventsByTable.fillerStop = [];
                                for (let event of this.getSpeedLosses.SLEVENTS) {
                                    if (this.dbNames.reducedRate.includes(event.reason)) {
                                        this.slEventsByTable.reducedRate.push(event);
                                    } else if (this.dbNames.fillerStop.includes(event.reason)) {
                                        this.slEventsByTable.fillerStop.push(event);
                                    }
                                }
                            }


                            //Total speed loss duration as the sum of all categorie's durations
                            const totalSpeedLossDuration = Object.values(this.slEvents).reduce((acc, slCat) => {
                                return acc + slCat.totalDuration;
                            }, 0);

                            //Calculate Speed Losses Percentage by Category
                            Object.values(this.slEvents).forEach(slCat => {
                                slCat.percentage = ((slCat.totalDuration / totalSpeedLossDuration)*100).toFixed(2);
                            });

                            const map = {
                                'Filler Own Stoppage': 'filler-stop',
                                'Filler Stop By Other Machine': 'filler-stop',
                                'Reduced Rate At Filler': 'reduced-rate',
                                'Reduced Rate At An Other Machine': 'reduced-rate'
                            }

                            //Update charts' data
                            let labels = {
                                'filler-stop': [
                                    this.$t("fillerOwnStop"),
                                    this.$t("fillerStopByOtherMachine")
                                ],
                                'reduced-rate': [
                                    this.$t("reduceRateAtFiller"),
                                    this.$t("reduceRateAtFillerDueToAnotherMachineCapacity")
                                ]
                            };
                            Object.keys(map).forEach(key => {
                                this.chartObjects[map[key]].chart.data.datasets[0].data = [];
                                this.chartObjects[map[key]].chart.data.datasets[1].data = [];
                            });
                            
                            Object.keys(map).forEach(key => {
                                this.chartObjects[map[key]].chart.data.labels = labels[map[key]];
                                this.chartObjects[map[key]].chart.data.datasets[0].data.push(chartData[key].duration);
                                this.chartObjects[map[key]].chart.data.datasets[1].data.push(chartData[key].freq);
                                this.chartObjects[map[key]].chart.update();
                            });
                        });

                    });
                }
            },

            createCharts: function() {
                this.chartObjects.created = true;
                for (let slCat of ['filler-stop', 'reduced-rate']) {
                    this.chartObjects[slCat].chart = new Chart(slCat + '-sl-chart', {
                    type: 'bar',
                    data: {
                        labels: [],
                        datasets: [{
                                label: this.$t("timeInMinutes"),
                                backgroundColor: 'rgb(245, 194, 67)',
                                data: []
                            },
                            {
                                label: this.$t("number"),
                                backgroundColor: 'rgb(90, 90, 90)',
                                data: [],
                                yAxisID: "freq"
                            }
                        ]
                    },
                    options: {
                        responsive: true,
                        maintainAspectRatio: false,
                        scales: {
                            time: {
                                axis: 'y',
                                title: {
                                    display: true,
                                    text: this.$t("timeInMinutes"),
                                },
                                position: 'left'
                            },
                            freq: {
                                axis: 'y',
                                title: {
                                    display: true,
                                    text: this.$t("number"),
                                },
                                position: 'right'
                            }
                        },
                        plugins: {
                            legend: {
                                display: true
                            }
                        }
                    },
                });
                }
            }
        },

        mounted() {

            if(sessionStorage.getItem("language") !== null){
                this.$i18n.locale = sessionStorage.getItem("language");
            }
            this.$store.dispatch('fetchSites');

            //Load chart.js into vue component
            let chartJs = document.createElement('script');
            chartJs.setAttribute('src', 'https://cdn.jsdelivr.net/npm/chart.js');
            document.head.appendChild(chartJs);


        },

        computed: {
            ...mapGetters(['sites', 'speedLoss', 'getSpeedLosses'])
        },

        components: {
            ProductionWindow
        },

        watch: {
            '$i18n.locale': function() {
                const labels = {
                    'own-stop': [
                        this.$t("fillerOwnStop"),
                        this.$t("fillerStopByOtherMachine")
                    ],
                    'other-machine': [
                        this.$t("reduceRateAtFiller"),
                        this.$t("reduceRateAtFillerDueToAnotherMachineCapacity")
                    ]
                };

                for (let slCat of ['own-stop', 'other-machine']) {
                    if (this.chartObjects[slCat]) {
                        this.chartObjects[slCat].chart.data.datasets[0].label = this.$t("timeInMinutes");
                        this.chartObjects[slCat].chart.data.datasets[1].label = this.$t("number");
                        this.chartObjects[slCat].chart.options.scales.time.title.text = this.$t("timeInMinutes");
                        this.chartObjects[slCat].chart.options.scales.freq.title.text = this.$t("number");
                        this.chartObjects[slCat].chart.data.labels = labels[slCat];
                        this.chartObjects[slCat].chart.update();
                    }
                }
            }
        }
    }
</script>
<style scoped>

    div.main-container {
        flex-direction: column;
        background-color: white;
        padding: 20px;
        min-width: 1000px;
        border-radius: 5px;
        margin: 20px 0px;
    }

    div.selection-menu {
        flex-direction: row;
        padding: 20px 0px;
        border-bottom: solid 1px;
        justify-content: space-between;
    }

    div.site-pl-selection {
        align-items: center;
    }

    div.site-pl-selection > *:not(label){
        height: 35px;
    }

    div.site-pl-selection label {
        margin: 0px 10px;
    }

    button#pl-selection-load {
        margin-left: 10px;
        background: rgb(245, 194, 67);
        color: rgb(90,90,90);
        border: none;
    }

    div.title-container {
        margin-top: 10px;
        display: flex;
        justify-content: center;
    }

    span.content-title {
        font-size: 24px;
        font-weight: bold;
        color: black;
        width: 100%;
        padding: 10px;
        text-align: center;
    }

    span.content-subtitle {
        font-size: 17px;
        font-weight: bold;
        color: black;
        width: 100%;
    }

    div.content-panel {
        display: flex;
        flex-wrap: wrap;
        flex-direction: row;
    }

    div.table-panel,
    div.chart-panel {
        display: flex;
        flex-direction: column;
        width: 50%;
        padding: 10px;
        border: solid 1px;
    }

    div.chart-panel {
        border-left: none;
    }

    div.table-container {
        height: 300px;
        overflow-y: scroll;
        margin: 10px 0px;
    }

    div.table-container th {
        text-align: center;
        border: none;
    }

    div.table-container th:first-of-type{
        border-top-left-radius: 7px;
        border-bottom-left-radius: 7px;
    }

    div.table-container th:last-of-type{
        border-top-right-radius: 7px;
        border-bottom-right-radius: 7px;
    }

    div.table-container td {
        text-align: center;
    }

    div.chart-container {
        height: 300px;
        margin: 10px 0px;
    }

    div.no-bottom-border {
        border-bottom: none;
    }

    div.rounded-top-left {
        border-top-left-radius: 5px;
    }

    div.rounded-top-right {
        border-top-right-radius: 5px;
    }

    div.rounded-bottom-left {
        border-bottom-left-radius: 5px;
    }

    div.rounded-bottom-right {
        border-bottom-right-radius: 5px;
    }

    table.table {
        margin: 10px 0px;
    }

    thead {
        color: rgb(90,90,90);
        background: rgb(245, 194, 67);
    }

</style>
