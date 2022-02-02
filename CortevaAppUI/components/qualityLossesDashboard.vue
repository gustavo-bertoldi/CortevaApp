<template>


    <div class="d-flex main-container">
        <!-- Interval, site and production line selection menu -->
        <div class="d-flex selection-menu">
            <!-- Site and production line selection-->
            <div class="d-flex site-pl-selection">
                <div class="d-flex">
                    <label for="site-selection">{{$t("site")}}: </label>
                    <select id="site-selection" v-model="site">
                        <option disabled selected value>-- Select --</option>
                        <template v-for="site of sites[0]">
                            <option v-bind:key="site.name" v-bind:value="site.name">{{site.name}}</option>
                        </template>
                    </select>
                </div>
                <div class="d-flex">
                    <label for="pl-selection">{{$t("productionLine")}}: </label>
                    <select id="pl-selection" v-on:change="productionLineSelected();">
                        <option disabled selected value>-- Select --</option>
                        <template v-for="productionLine of sites[1]">
                            <template v-if="productionLine.name === site">
                                <option v-bind:key="productionLine.productionline_name" v-bind:value="productionLine.productionline_name">
                                    {{productionLine.productionline_name}}
                                </option>
                            </template>
                        </template>
                    </select>
                </div>

                <div class="d-flex">
                    <input v-on:click="load()" type="button" class="btn btn-outline-info" v-bind:value="lo">
                </div>

            </div>
        </div>


        <div class="content-panel">
            <div class="upper-panel">
                <span class="content-title">{{$t("qualityLosses")}}</span>
                <div class="chart-panel">

                </div>
            </div>
            <div class="bottom-panel">
                <div class="ql-machine-panel">
                    <span class="content-title">{{$t("qualityLossesByMachine")}}</span>
                    <div class="inner-content-panel">
                        <div class="table-panel">

                        </div>
                        <div class="chart-panel">

                        </div>
                    </div>
                </div>
                <div class="ql-format-panel">
                    <span class="content-title">{{$t("qualityLossesByFormat")}}</span>
                    <div class="inner-content-panel">
                        <div class="table-panel">

                        </div>
                        <div class="chart-panel">

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import {mapGetters} from "vuex";
    import ProductionWindow from './productionWindow.vue';

    export default {
        name: "qualityLossesDashboard",

        data() {

            var data = {

                currentYear: (new Date()).getFullYear(),
                site: '',
                productionLine: '',
                lo : this.$t("load"),
            };

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

            chargeData: function(dateFrom, dateTo) {
                const selectedPL = document.getElementById('pl-selection').value;

            },

            createCharts: function() {
                this.chartObjects.created = true;
                for (let stopCause of ['own-stop', 'otherMachine']) {
                    this.chartObjects[stopCause] = new Chart(stopCause + '-shutdown-chart', {
                    type: 'bar',
                    data: {
                        labels: [],
                        datasets: [{
                                label: 'Time in minutes',
                                backgroundColor: 'rgb(112, 184, 232)',
                                data: []
                            },
                            {
                                label: 'Number',
                                backgroundColor: 'rgb(246, 184, 192)',
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
                                    text: 'Time in minutes'
                                },
                                position: 'left'
                            },
                            freq: {
                                axis: 'y',
                                title: {
                                    display: true,
                                    text: 'Number'
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

            var today = new Date();
            var dd = today.getDate();
            var mm = today.getMonth() + 1; //January is 0!
            var yyyy = today.getFullYear();

            if (dd < 10) {
                dd = '0' + dd;
            }

            if (mm < 10) {
                mm = '0' + mm;
            }

            today = yyyy + '-' + mm + '-' + dd;
            document.getElementById("startingPO").setAttribute("max", today);
            document.getElementById("endingPO").setAttribute("max", today);


            //Load chart.js into vue component
            let chartJs = document.createElement('script');
            chartJs.setAttribute('src', 'https://cdn.jsdelivr.net/npm/chart.js');
            document.head.appendChild(chartJs);
        },

        computed: {
            ...mapGetters(['sites', 'speedLoss'])
        },

        components: {
            ProductionWindow
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
    }

    div.site-pl-selection {
        flex-direction: column;
        justify-content: space-evenly;
        min-width: 200px;
    }

    div.site-pl-selection > div{
        align-items: center;
    }

    div.site-pl-selection select {
        width: 100%;
    }

    div.site-pl-selection label {
        margin: 0px 10px 0px 0px;
    }

    div.title-container {
        margin-top: 10px;
        display: flex;
        justify-content: center;
    }

    span.content-title {
        font-size: 20px;
        font-weight: bold;
        color: black;
        width: 100%;
    }

    span.content-subtitle {
        font-size: 17px;
        font-weight: bold;
        color: black;
        width: 100%;
    }

    div.content-panel {
        display: flex;
        flex-direction: column;
    }

    div.upper-panel,
    div.bottom-panel {
        display: flex;
        width: 100%;
    }

    div.ql-machine-panel,
    div.ql-format-panel {
        width: 50%;
    }


    table.table {
        margin: 10px 0px;
    }



</style>
