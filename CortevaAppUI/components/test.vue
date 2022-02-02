<template>
    <div class="" id="component">
        <div class="row">
            <div class="col-sm">
                <div class="">

                    <form>
                        <label class="" for="site">{{$t("site")}} : </label>
                        <select name="site" id="site" class="form-select" v-model="site">
                            <template v-for="site in sites[0]">
                                <option v-bind:value="site.name">
                                    {{site.name}}
                                </option>
                            </template>
                        </select>
                    </form>


                </div>


                <div class="">
                    <form>
                        <label class="" for="productionline">{{$t("productionLine")}} : </label>
                        <select name="productionline" id="productionline" class="form-select" v-model="productionline">
                            <template v-for="productionline in sites[1]">
                                <template v-if="productionline.name === site">
                                    <option v-bind:value="productionline.productionline_name">
                                        {{productionline.productionline_name}}
                                    </option>
                                </template>

                            </template>

                        </select>
                    </form>
                </div>


                <br/>
                <input v-on:click="load()" type="button" class="btn btn-outline-info" v-bind:value="lo">

                <br/>

            </div>


            <div class="col-sm">
                <h1>
                    {{$t("productionDashboard")}}
                </h1>

                <br/>
                <div class="">
                    <label class="" for="startingPO">{{$t("from")}}</label>
                    <input type="date" id="startingPO" class=" " required v-model="beginningDate">


                    <label class="" for="endingPO">{{$t("to")}}</label>
                    <input type="date" id="endingPO" class=""
                           required v-model="endingDate">
                </div>


            </div>
        </div>


        <div class="row">
            <div class="col-sm">
                <template v-if="show===1">
                    <h1>
                        {{$t("formulationSplit")}}
                    </h1>


                    <div class="table-info-data" width="400">
                        <table class="table">
                            <thead>
                            <tr>
                                <th scope="col"></th>
                                <th scope="col">Jan</th>
                                <th scope="col">Feb</th>
                                <th scope="col">Mar</th>
                                <th scope="col">Apr</th>
                                <th scope="col">May</th>
                                <th scope="col">Jun</th>
                                <th scope="col">Jul</th>
                                <th scope="col">Aug</th>
                                <th scope="col">Sep</th>
                                <th scope="col">Oct</th>
                                <th scope="col">Nov</th>
                                <th scope="col">Dec</th>


                            </tr>
                            </thead>
                            <tbody>
                            <tr v-for="(formulation, index) in formulations" :key="index">
                                <th scope="row">{{formulation}}</th>
                                <td>{{formulationsPerMonth[index]['January']}}</td>
                                <td>{{formulationsPerMonth[index]['February']}}</td>
                                <td>{{formulationsPerMonth[index]['March']}}</td>
                                <td>{{formulationsPerMonth[index]['April']}}</td>
                                <td>{{formulationsPerMonth[index]['May']}}</td>
                                <td>{{formulationsPerMonth[index]['June']}}</td>
                                <td>{{formulationsPerMonth[index]['July']}}</td>
                                <td>{{formulationsPerMonth[index]['August']}}</td>
                                <td>{{formulationsPerMonth[index]['September']}}</td>
                                <td>{{formulationsPerMonth[index]['October']}}</td>
                                <td>{{formulationsPerMonth[index]['November']}}</td>
                                <td>{{formulationsPerMonth[index]['December']}}</td>
                            </tr>
                            <tr>
                                <th scope="row"></th>
                                <td>{{sumPerMonth['January']}}</td>
                                <td>{{sumPerMonth['February']}}</td>
                                <td>{{sumPerMonth['March']}}</td>
                                <td>{{sumPerMonth['April']}}</td>
                                <td>{{sumPerMonth['May']}}</td>
                                <td>{{sumPerMonth['June']}}</td>
                                <td>{{sumPerMonth['July']}}</td>
                                <td>{{sumPerMonth['August']}}</td>
                                <td>{{sumPerMonth['September']}}</td>
                                <td>{{sumPerMonth['October']}}</td>
                                <td>{{sumPerMonth['November']}}</td>
                                <td>{{sumPerMonth['December']}}</td>
                            </tr>

                            </tbody>
                        </table>

                    </div>
                </template>

            </div>

            <div class="col-sm">


                <template v-if="show===1">
                    <h1>
                        {{$t("formulationSplit")}} (%)
                    </h1>
                </template>
                <canvas id="formulationSplit" width="100" height="100"/>


            </div>
        </div>

        <div class="row">

            <div class="wrapper">
                <canvas id="myChart4"></canvas>
            </div>
        </div>


        <div class="row">
            <div class="col-sm">

                <template v-if="show===1">


                    <h1>
                        {{$t("packSizeSplit")}}
                    </h1>

                    <div class="table-info-data" width="400">
                        <table class="table">
                            <thead>
                            <tr>
                                <th scope="col"></th>
                                <th scope="col">Jan</th>
                                <th scope="col">Feb</th>
                                <th scope="col">Mar</th>
                                <th scope="col">Apr</th>
                                <th scope="col">May</th>
                                <th scope="col">Jun</th>
                                <th scope="col">Jul</th>
                                <th scope="col">Aug</th>
                                <th scope="col">Sep</th>
                                <th scope="col">Oct</th>
                                <th scope="col">Nov</th>
                                <th scope="col">Dec</th>


                            </tr>
                            </thead>
                            <tbody>
                            <tr v-for="(packsize, index) in packsizes" :key="index">
                                <th scope="row">{{packsize}}L</th>
                                <td>{{packsizePerMonth[index]['January']}}</td>
                                <td>{{packsizePerMonth[index]['February']}}</td>
                                <td>{{packsizePerMonth[index]['March']}}</td>
                                <td>{{packsizePerMonth[index]['April']}}</td>
                                <td>{{packsizePerMonth[index]['May']}}</td>
                                <td>{{packsizePerMonth[index]['June']}}</td>
                                <td>{{packsizePerMonth[index]['July']}}</td>
                                <td>{{packsizePerMonth[index]['August']}}</td>
                                <td>{{packsizePerMonth[index]['September']}}</td>
                                <td>{{packsizePerMonth[index]['October']}}</td>
                                <td>{{packsizePerMonth[index]['November']}}</td>
                                <td>{{packsizePerMonth[index]['December']}}</td>
                            </tr>
                            <tr>
                                <th scope="row"></th>
                                <td>{{sumPerMonth2['January']}}</td>
                                <td>{{sumPerMonth2['February']}}</td>
                                <td>{{sumPerMonth2['March']}}</td>
                                <td>{{sumPerMonth2['April']}}</td>
                                <td>{{sumPerMonth2['May']}}</td>
                                <td>{{sumPerMonth2['June']}}</td>
                                <td>{{sumPerMonth2['July']}}</td>
                                <td>{{sumPerMonth2['August']}}</td>
                                <td>{{sumPerMonth2['September']}}</td>
                                <td>{{sumPerMonth2['October']}}</td>
                                <td>{{sumPerMonth2['November']}}</td>
                                <td>{{sumPerMonth2['December']}}</td>
                            </tr>

                            </tbody>
                        </table>

                    </div>


                </template>

                <br/>

            </div>
            <div class="col-sm">

                <template v-if="show===1">
                    <h1>
                        {{$t("formatSplit")}} (%)
                    </h1>
                </template>
                <canvas id="packsizeSplit" width="100" height="100"/>

            </div>
        </div>

        <div class="row">

            <div class="wrapper">
                <canvas id="myChart5"></canvas>
            </div>
        </div>
    </div>
</template>

<script>
    import {mapGetters} from "vuex";

    export default {
        name: "test",

        data() {
            return {
                lo: this.$t("load"),

                site: '',
                beginningDate: '',
                endingDate: '',
                productionline: '',
                show: 0,


                formulations: '',
                formulationsPerMonth: '',
                qtyPerFormulation: [],
                sumPerMonth: [],
                total: 0,

                packsizes: [],
                total2: 0,
                sumPerMonth2: [],
                packsizePerMonth: [],

            }
        },

        methods: {
            load: async function () {
                var index = 0;
                for (let i = 0; i < this.sites[1].length; i++) {

                    if (this.sites[1][i].productionline_name === this.productionline) {
                        index = this.sites[1][i].id;
                    }
                }


                if (this.productionline !== '' && this.beginningDate !== '' && this.endingDate !== '') {
                    var tab = [];
                    tab.push(this.site);
                    tab.push(this.productionline);
                    tab.push(this.beginningDate);
                    tab.push(this.endingDate);

                    this.$store.dispatch('fetchVolumes', tab);
                    await this.resolveAfter15Second();

                    this.makeCalculationFormulation();
                    this.pieCharts();
                    this.pieCharts2();

                    this.graph();
                    this.graph2();

                    this.show = 1;


                }
            },

            resolveAfter15Second: function () {
                return new Promise(resolve => {
                    setTimeout(() => {
                        resolve('resolved');
                    }, 1500);
                });
            },

            graph: function () {
                var ctx = document.getElementById("myChart4").getContext('2d');
                var tab = [];
                var colors = [];
                colors.push("#caf270");
                colors.push("#45c490");
                colors.push("#008d93");
                colors.push("#2e5468");


                for (let i = 0; i < this.formulations.length; i++) {
                    let mois = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    var ar = [];
                    for (let j = 0; j < mois.length; j++) {
                        ar.push(this.formulationsPerMonth[i][mois[j]]);
                    }
                    var obj = {
                        label: this.formulations[i],
                        backgroundColor: colors[i],
                        data: ar,
                    };

                    tab.push(obj);
                }
                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                        datasets: tab,

                    },
                    options: {
                        tooltips: {
                            displayColors: true,
                            callbacks: {
                                mode: 'x',
                            },
                        },
                        scales: {
                            xAxes: [{
                                stacked: true,
                                gridLines: {
                                    display: false,
                                }
                            }],
                            yAxes: [{
                                stacked: true,
                                ticks: {
                                    beginAtZero: true,
                                },
                                type: 'linear',
                            }]
                        },
                        responsive: true,
                        maintainAspectRatio: false,
                        legend: {position: 'bottom'},
                    }
                });

            },

            pieCharts: function () {
                var data = [];
                for (let j = 0; j < this.formulations.length; j++) {
                    let formulation = this.formulations[j];
                    let sum = 0;
                    //console.log('PER MONTH');

                    //console.log(this.formulationsPerMonth[j]);
                    let mois = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];


                    for (let k = 0; k < mois.length; k++) {
                        let m = mois[k];
                        sum += this.formulationsPerMonth[j][m];
                    }

                    var obj = {
                        name: formulation,
                        nbr: sum
                    };
                    data.push(obj);
                }
                console.log('DATA');

                console.log(data);


                const randomHexColorCode = () => {
                    return "#" + Math.random().toString(16).slice(2, 8)
                };

                var canvas = document.getElementById("formulationSplit");
                var ctx = canvas.getContext("2d");
                canvas.width = 450;
                canvas.height = 450;
                let total = data.reduce((ttl, house) => {
                    return ttl + house.nbr
                }, 0);
                let startAngle = 0;
                let radius = 100;
                let cx = canvas.width / 2;
                let cy = canvas.height / 2;

                for (let j = 0; j < data.length; j++) {

                    let item = data[j];
                    //set the styles before beginPath
                    ctx.fillStyle = randomHexColorCode();
                    ctx.lineWidth = 1;
                    ctx.strokeStyle = '#333';
                    ctx.beginPath();
                    //console.log(total, house.troops, house.troops/total);
                    // draw the pie wedges
                    let endAngle = ((item.nbr / total) * Math.PI * 2) + startAngle;
                    ctx.moveTo(cx, cy);
                    ctx.arc(cx, cy, radius, startAngle, endAngle, false);
                    ctx.lineTo(cx, cy);
                    ctx.fill();
                    ctx.stroke();
                    ctx.closePath();

                    // add the labels
                    ctx.beginPath();
                    ctx.font = '15px Helvetica, Calibri';
                    ctx.textAlign = 'center';
                    ctx.fillStyle = 'rebeccapurple';
                    // midpoint between the two angles
                    // 1.5 * radius is the length of the Hypotenuse
                    let theta = (startAngle + endAngle) / 2;
                    let deltaY = Math.sin(theta) * 1.5 * radius;
                    let deltaX = Math.cos(theta) * 1.5 * radius;
                    /***
                     SOH  - sin(angle) = opposite / hypotenuse
                     = opposite / 1px
                     CAH  - cos(angle) = adjacent / hypotenuse
                     = adjacent / 1px
                     TOA

                     ***/
                    var txt = item.name + '\n';
                    var pct = item.nbr / this.total * 100;
                    txt = txt + ' ' + pct.toFixed(2) + '%';
                    ctx.fillText(txt, deltaX + cx, deltaY + cy);
                    ctx.closePath();

                    startAngle = endAngle;
                }


            },

            graph2: function () {
                var ctx = document.getElementById("myChart5").getContext('2d');
                var tab = [];
                var colors = [];
                colors.push("#caf270");
                colors.push("#45c490");
                colors.push("#008d93");
                colors.push("#2e5468");


                for (let i = 0; i < this.packsizes.length; i++) {
                    let mois = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    var ar = [];
                    for (let j = 0; j < mois.length; j++) {
                        ar.push(this.packsizePerMonth[i][mois[j]]);
                    }
                    var obj = {
                        label: this.packsizes[i] + 'L',
                        backgroundColor: colors[i],
                        data: ar,
                    };

                    tab.push(obj);
                }
                console.log('tab');

                console.log(tab);

                var myChart = new Chart(ctx, {
                    type: 'bar',
                    data: {
                        labels: ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"],
                        datasets: tab,

                    },
                    options: {
                        tooltips: {
                            displayColors: true,
                            callbacks: {
                                mode: 'x',
                            },
                        },
                        scales: {
                            xAxes: [{
                                stacked: true,
                                gridLines: {
                                    display: false,
                                }
                            }],
                            yAxes: [{
                                stacked: true,
                                ticks: {
                                    beginAtZero: true,
                                },
                                type: 'linear',
                            }]
                        },
                        responsive: true,
                        maintainAspectRatio: false,
                        legend: {position: 'bottom'},
                    }
                });

            },


            pieCharts2: function () {
                var data = [];
                for (let j = 0; j < this.packsizes.length; j++) {
                    let format = this.packsizes[j];
                    let sum = 0;
                    //console.log('PER MONTH');

                    //console.log(this.formulationsPerMonth[j]);
                    let mois = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];


                    for (let k = 0; k < mois.length; k++) {
                        let m = mois[k];
                        sum += this.packsizePerMonth[j][m];
                    }

                    var obj = {
                        name: format,
                        nbr: sum
                    };
                    data.push(obj);
                }
                console.log('DATA');

                console.log(data);


                const randomHexColorCode = () => {
                    return "#" + Math.random().toString(16).slice(2, 8)
                };

                var canvas = document.getElementById("packsizeSplit");
                var ctx = canvas.getContext("2d");
                canvas.width = 450;
                canvas.height = 450;
                let total = data.reduce((ttl, house) => {
                    return ttl + house.nbr
                }, 0);
                let startAngle = 0;
                let radius = 100;
                let cx = canvas.width / 2;
                let cy = canvas.height / 2;

                for (let j = 0; j < data.length; j++) {

                    let item = data[j];
                    //set the styles before beginPath
                    ctx.fillStyle = randomHexColorCode();
                    ctx.lineWidth = 1;
                    ctx.strokeStyle = '#333';
                    ctx.beginPath();
                    //console.log(total, house.troops, house.troops/total);
                    // draw the pie wedges
                    let endAngle = ((item.nbr / total) * Math.PI * 2) + startAngle;
                    ctx.moveTo(cx, cy);
                    ctx.arc(cx, cy, radius, startAngle, endAngle, false);
                    ctx.lineTo(cx, cy);
                    ctx.fill();
                    ctx.stroke();
                    ctx.closePath();

                    // add the labels
                    ctx.beginPath();
                    ctx.font = '15px Helvetica, Calibri';
                    ctx.textAlign = 'center';
                    ctx.fillStyle = 'rebeccapurple';
                    // midpoint between the two angles
                    // 1.5 * radius is the length of the Hypotenuse
                    let theta = (startAngle + endAngle) / 2;
                    let deltaY = Math.sin(theta) * 1.5 * radius;
                    let deltaX = Math.cos(theta) * 1.5 * radius;

                    var txt = item.name + 'L \n';
                    var pct = item.nbr / this.total2 * 100;
                    txt = txt + ' ' + pct.toFixed(2) + '%';
                    ctx.fillText(txt, deltaX + cx, deltaY + cy);
                    ctx.closePath();

                    startAngle = endAngle;
                }


            },


            makeCalculationFormulation: function () {

                var sommeQtyProduced = 0;
                this.formulations = [];

                var qtyPerFormulation = [];

                for (let i = 0; i < this.volumes.length; i++) {
                    sommeQtyProduced += this.volumes[i].qtyProduced * this.volumes[i].bottlesPerCase * this.volumes[i].size * 1;
                    if (!this.formulations.includes(this.volumes[i].BULK)) {
                        this.formulations.push(this.volumes[i].BULK);
                        qtyPerFormulation[this.volumes[i].BULK] = this.volumes[i].qtyProduced * this.volumes[i].bottlesPerCase * this.volumes[i].size * 1;
                    } else {
                        qtyPerFormulation[this.volumes[i].BULK] += this.volumes[i].qtyProduced * this.volumes[i].bottlesPerCase * this.volumes[i].size * 1;
                    }
                }

                var finalValue = [];
                for (let j = 0; j < this.formulations.length; j++) {
                    var tableauFormulation = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    var tableauFormulationValue = [];
                    for (let k = 0; k < tableauFormulation.length; k++) {
                        tableauFormulationValue[tableauFormulation[k]] = 0;
                    }


                    for (let i = 0; i < this.volumes.length; i++) {
                        if (this.volumes[i].BULK === this.formulations[j]) {
                            var month = this.volumes[i].created_at.split('-')[1];
                            console.log('MOIS : ' + month);

                            var correspondingMonth = tableauFormulation[month - 1];
                            tableauFormulationValue[correspondingMonth] += this.volumes[i].qtyProduced * this.volumes[i].bottlesPerCase * this.volumes[i].size * 1;
                        }
                    }

                    finalValue.push(tableauFormulationValue);
                }

                let l = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                let tab = [];
                for (let k = 0; k < l.length; k++) {
                    tab[l[k]] = 0;
                }
                for (let t = 0; t < finalValue.length; t++) {

                    for (let k = 0; k < l.length; k++) {
                        let month = l[k];
                        tab[month] += finalValue[t][month];
                        this.total += finalValue[t][month];
                    }

                }
                this.sumPerMonth = tab;
                this.formulationsPerMonth = finalValue;


                //Graph2
                var sommeQtyProduced2 = 0;
                this.packsizes = [];

                var qtyPerPacksize = [];

                for (let i = 0; i < this.volumes.length; i++) {
                    sommeQtyProduced2 += this.volumes[i].qtyProduced * this.volumes[i].bottlesPerCase * 1;
                    if (!this.packsizes.includes(this.volumes[i].size)) {
                        this.packsizes.push(this.volumes[i].size);
                        qtyPerPacksize[this.volumes[i].size] = this.volumes[i].qtyProduced * this.volumes[i].bottlesPerCase * 1;
                    } else {
                        qtyPerPacksize[this.volumes[i].size] += this.volumes[i].qtyProduced * this.volumes[i].bottlesPerCase * 1;
                    }
                }

                var finalValue2 = [];
                for (let j = 0; j < this.packsizes.length; j++) {
                    tableauFormulation = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                    var tableauPacksizeValue = [];
                    for (let k = 0; k < tableauFormulation.length; k++) {
                        tableauPacksizeValue[tableauFormulation[k]] = 0;
                    }


                    for (let i = 0; i < this.volumes.length; i++) {
                        if (this.volumes[i].size === this.packsizes[j]) {
                            month = this.volumes[i].created_at.split('-')[1];

                            correspondingMonth = tableauFormulation[month - 1];
                            tableauPacksizeValue[correspondingMonth] += this.volumes[i].qtyProduced * this.volumes[i].bottlesPerCase * 1;
                        }
                    }

                    finalValue2.push(tableauPacksizeValue);
                }

                l = ["January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"];
                tab = [];
                for (let k = 0; k < l.length; k++) {
                    tab[l[k]] = 0;
                }
                for (let t = 0; t < finalValue2.length; t++) {
                    for (let k = 0; k < l.length; k++) {
                        let month = l[k];
                        tab[month] += finalValue2[t][month];
                        this.total2 += finalValue2[t][month];
                    }

                }
                this.sumPerMonth2 = tab;
                this.packsizePerMonth = finalValue2;

                console.log(this.sumPerMonth2);
                console.log(this.sumPerMonth);


            }

        },


        mounted() {
            if (sessionStorage.getItem("language") !== null) {
                this.$i18n.locale = sessionStorage.getItem("language");
            }
            this.$store.dispatch('fetchSites');
            let chartJs = document.createElement('script');
            chartJs.setAttribute('src', 'https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.3.0/Chart.min.js');
            document.head.appendChild(chartJs);

        },

        computed: {
            ...mapGetters([
                'sites',
                'volumes',
            ])
        }
    }
</script>

<style scoped>
    h1 {
        font-size: 1.4em;
        color: #56baed;
    }

    label {
        font-size: 1.4em;
        color: #56baed;
    }

    p {
        font-size: 1.4em;
        color: #56baed;
    }

    h2 {
        font-size: 1.2em;
        color: #56baed;
    }

    h4 {
        color: red;
    }


    div {
        background-color: #fff;
        padding: 15px;
    }

    thead {
        color: white;
        background: #56baed;
    }

    .container {
        margin-left: 60px;
    }

    h5 {
        margin-left: 60px;
    }


    .table-info-data {
        overflow: scroll;
        max-height: 450px;
        max-width: 400px;
    }


    .wrapper {
        width: 60%;
        display: block;
        overflow: hidden;
        margin: 0 auto;
        padding: 60px 50px;
        background: #fff;
        border-radius: 4px;
    }

    canvas {
        background: #fff;
        height: 400px;
    }


</style>
