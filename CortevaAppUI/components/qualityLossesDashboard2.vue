<template>

    <div>
        <!--
                {{ this.qualityLosses['formats']}}

                {{allEvents['SITE']}}-->
        <div class="row dataInput">
            <div class="col-sm">
                <div class="data">
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
                    <div>
                        <form>
                            <label class="" for="productionline">{{$t("productionLine")}} : </label>
                            <select name="productionline" id="productionline" class="form-select"
                                    v-model="productionline">
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
                    <div>
                        <input v-on:click="load()" type="button" class="btn btn-outline-info" v-bind:value="lo">
                    </div>

                </div>
            </div>

            <div class="col-sm">
                <div class=data>
                    <div>

                        <h1>
                            {{$t("qualityLossesDashboard")}}
                        </h1>
                    </div>
                    <div>
                        <label class="" for="startingPO">{{$t("from")}}</label>
                        <input type="date" id="startingPO" class=" " required v-model="beginningDate">


                        <label class="" for="endingPO">{{$t("to")}}</label>
                        <input type="date" id="endingPO" class=""
                               required v-model="endingDate">
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm">

                <span class="content-title">
                    {{$t("qualityLosses")}}
                </span>
            </div>
        </div>

        <div class="row">
            <div class="col-sm">
                <img src="images/qualityLossesDiagram.png">
            </div>
            <div class="col-sm  border border-primary">
                <h3 style="padding-top : 55px;">
                    <template v-if="show === 1">
                        <!--QL = {{(quality*100).toFixed(2)}} %-->
                        QL = 100 %
                    </template>
                </h3>
            </div>

        </div>
        <div class="row">
            <div class="col-sm border border-primary" style="padding-left : 50px;">
                <p>
                    N = Produced Pieces in Specification during the production run <br/>
                    RMi, RF = Rejected Pieces at the Filler or machines Mi <br/>
                    CMi = Machine counter value at end of production run
                </p>
            </div>
            <div class="col-sm border border-primary">
                <p>
                    M1 = telle machine <br/>
                    M2 = telle machine <br/>
                    M3 = telle machine <br/>
                    M4 = telle machine <br/>

                </p>
            </div>

        </div>

        <div class="row">
            <div class="col-sm">
                <span class="content-title">{{$t("qualityLossesByMachine")}}</span>
                <!-- <template v-if="show===1">
                     <h1>
                         {{$t("qualityImpactPerMachine")}}
                     </h1>
                 </template>-->
                <canvas id="qualityLossesByMachines" width="100" height="100"/>
            </div>
            <div class="col-sm">
                <span class="content-title">
                    {{$t("qualityLossesByFormat")}}
                </span>
                <!--<template v-if="show===1">
                    <h1>
                        {{$t("qualityImpactByFormat")}}
                    </h1>
                </template>-->
                <canvas id="qualityLossesByFormat" width="100" height="100"/>
            </div>
        </div>

        <div class="row">

            <div class="col-sm">
                <template v-if="show === 1">

                    <table class="table">
                        <thead>
                        <tr>
                            <th scope="col"></th>
                            <th scope="col">{{$t("numberOfItems")}}</th>
                        </tr>
                        </thead>
                        <tbody>


                        <tr class="filler mainLine">
                            <th scope="row">{{$t("filler")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumFillerCounter*1 +
                                qualityLosses['rejectionCounter'][0].sumFillerRejection*1}}
                            </td>
                        </tr>

                        <tr class="filler">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumFillerCounter}}</td>
                        </tr>

                        <tr class="filler">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumFillerRejection}}</td>
                        </tr>


                        <tr class="caper mainLine">
                            <th scope="row">{{$t("caper")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumCaperCounter*1 +
                                qualityLosses['rejectionCounter'][0].sumCaperRejection*1}}
                            </td>
                        </tr>

                        <tr class="caper">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumCaperCounter}}</td>
                        </tr>

                        <tr class="caper">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumCaperRejection}}</td>
                        </tr>


                        <tr class="labeler mainLine">
                            <th scope="row">{{$t("labeler")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumLabelerCounter*1 +
                                qualityLosses['rejectionCounter'][0].sumLabelerRejection*1}}
                            </td>
                        </tr>

                        <tr class="labeler">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumLabelerCounter}}</td>
                        </tr>

                        <tr class="labeler">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumLabelerRejection}}</td>
                        </tr>

                        <tr class="boxWeigher mainLine">
                            <th scope="row">{{$t("boxWeigher")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumWeightBoxCounter*1 +
                                qualityLosses['rejectionCounter'][0].sumWeightBoxRejection*1}}
                            </td>
                        </tr>

                        <tr class="boxWeigher">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumWeightBoxCounter}}</td>
                        </tr>

                        <tr class="boxWeigher">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumWeightBoxRejection}}</td>
                        </tr>

                        <tr class="qualityControl mainLine">
                            <th scope="row">{{$t("qualityControl")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumQualityControlCounter*1 +
                                qualityLosses['rejectionCounter'][0].sumQualityControlRejection*1}}
                            </td>
                        </tr>

                        <tr class="qualityControl">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumQualityControlCounter}}</td>
                        </tr>

                        <tr class="qualityControl">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td>{{qualityLosses['rejectionCounter'][0].sumQualityControlRejection}}</td>
                        </tr>

                        <tr class="totalQty">
                            <th scope="row">{{$t("total")}}</th>
                            <td>{{totalItems}}</td>
                        </tr>


                        </tbody>
                    </table>
                </template>
            </div>

            <div class="col-sm">

                <template v-if="show === 1">

                    <table class="table">
                        <thead>
                        <tr>
                            <th scope="col"></th>
                            <th v-for="format in tableauFormats" scope="col">{{format}}L</th>
                        </tr>
                        </thead>
                        <tbody>

                        <tr class="filler mainLine">
                            <th scope="row">{{$t("filler")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['fillerCounter'][format] +
                                qtyPerMachine['fillerRejection'][format]}}
                            </td>

                        </tr>

                        <tr class="filler">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['fillerCounter'][format]}}
                            </td>
                        </tr>

                        <tr class="filler">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['fillerRejection'][format]}}
                            </td>
                        </tr>

                        <tr class="caper mainLine">
                            <th scope="row">{{$t("caper")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['caperCounter'][format] +
                                qtyPerMachine['caperRejection'][format]}}
                            </td>
                        </tr>

                        <tr class="caper">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['caperCounter'][format]}}
                            </td>
                        </tr>

                        <tr class="caper">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['caperRejection'][format]}}
                            </td>
                        </tr>


                        <tr class="labeler mainLine">
                            <th scope="row">{{$t("labeler")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['labelerCounter'][format] +
                                qtyPerMachine['labelerRejection'][format]}}
                            </td>
                        </tr>

                        <tr class="labeler">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['fillerCounter'][format]}}
                            </td>
                        </tr>

                        <tr class="labeler">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['fillerRejection'][format]}}
                            </td>
                        </tr>

                        <tr class="boxWeigher mainLine">
                            <th scope="row">{{$t("boxWeigher")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['weightBoxCounter'][format] +
                                qtyPerMachine['weightBoxRejection'][format]}}
                            </td>

                        </tr>

                        <tr class="boxWeigher">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['weightBoxCounter'][format]}}
                            </td>
                        </tr>

                        <tr class="boxWeigher">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['weightBoxRejection'][format]}}
                            </td>
                        </tr>

                        <tr class="qualityControl mainLine">
                            <th scope="row">{{$t("qualityControl")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine["controlQualityCounter"][format] +
                                qtyPerMachine["controlQualityRejection"][format]}}
                            </td>

                        </tr>

                        <tr class="qualityControl">
                            <th scope="row">{{$t("overProcess")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine['controlQualityCounter'][format]}}
                            </td>
                        </tr>

                        <tr class="qualityControl">
                            <th scope="row">{{$t("rejectedItems")}}</th>
                            <td v-for="format in tableauFormats">
                                {{qtyPerMachine["controlQualityRejection"][format]}}
                            </td>
                        </tr>


                        <tr class="totalQty">
                            <th scope="row">{{$t("total")}}</th>
                            <td v-for="format in tableauFormats">
                                {{totalPerFormat[format]}}
                            </td>
                        </tr>


                        </tbody>
                    </table>
                </template>
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
                lo: this.$t("load"),
                beginningDate: "",
                endingDate: "",
                productionline: "",
                show: 0,
                totalItems: 0,
                tableauFormats: [],
                qtyPerMachine: [],
                totalPerFormat: [],
                qualityLossesPerMachineArray: [],
                quality: 0
            };

            return data;
        },

        methods: {
            resolveAfter: function (milliseconds) {
                return new Promise(resolve => {
                    setTimeout(() => resolve(), milliseconds);
                });
            },


            resolveAfter15Second: function () {
                return new Promise(resolve => {
                    setTimeout(() => {
                        resolve('resolved');
                    }, 1500);
                });
            },


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


                    console.log(this.beginningDate);
                    this.$store.dispatch('fetchAllEvents', tab);

                    this.$store.dispatch('fetchQualityLosses', tab);
                    await this.resolveAfter15Second();
                    await this.resolveAfter15Second();

                    this.show = 1;
                    this.loadQualityLosses();
                    this.qualityLossesPerMachineArray = [];
                    this.qualityLossesPerMachineArray.push({
                        name: "weightBox",
                        value: this.qualityLosses['rejectionCounter'][0].sumWeightBoxCounter * 1 +
                            this.qualityLosses['rejectionCounter'][0].sumWeightBoxRejection * 1
                    });

                    this.qualityLossesPerMachineArray.push({
                        name: "caper",
                        value: this.qualityLosses['rejectionCounter'][0].sumCaperCounter * 1 +
                            this.qualityLosses['rejectionCounter'][0].sumCaperRejection * 1
                    });

                    this.qualityLossesPerMachineArray.push({
                        name: "filler",
                        value: this.qualityLosses['rejectionCounter'][0].sumFillerCounter * 1 +
                            this.qualityLosses['rejectionCounter'][0].sumFillerRejection * 1
                    });

                    this.qualityLossesPerMachineArray.push({
                        name: "labeler",
                        value: this.qualityLosses['rejectionCounter'][0].sumLabelerCounter * 1 +
                            this.qualityLosses['rejectionCounter'][0].sumLabelerRejection * 1
                    });

                    this.qualityLossesPerMachineArray.push({
                        name: "qualityControl",
                        value: this.qualityLosses['rejectionCounter'][0].sumQualityControlCounter * 1 +
                            this.qualityLosses['rejectionCounter'][0].sumQualityControlRejection * 1
                    });


                    this.totalItems = this.qualityLosses['rejectionCounter'][0].sumWeightBoxCounter * 1 +
                        this.qualityLosses['rejectionCounter'][0].sumWeightBoxRejection * 1 +
                        this.qualityLosses['rejectionCounter'][0].sumCaperCounter * 1 +
                        this.qualityLosses['rejectionCounter'][0].sumCaperRejection * 1 +
                        this.qualityLosses['rejectionCounter'][0].sumFillerCounter * 1 +
                        this.qualityLosses['rejectionCounter'][0].sumFillerRejection * 1 +
                        this.qualityLosses['rejectionCounter'][0].sumLabelerCounter * 1 +
                        this.qualityLosses['rejectionCounter'][0].sumLabelerRejection * 1 +
                        this.qualityLosses['rejectionCounter'][0].sumQualityControlCounter * 1 +
                        this.qualityLosses['rejectionCounter'][0].sumQualityControlRejection * 1
                    ;

                    this.loadQualityLossesByFormat();
                    this.pieCharts();


                }
            },

            loadQualityLosses: function () {


                var sommeQtyProduced = 0;
                var sommeRejection = 0;

                var fillerCounter = 0;
                var caperCounter = 0;
                var labelerCounter = 0;
                var wieghtBoxCounter = 0;
                var qualityControlCounter = 0;


                for (let i = 0; i < this.allEvents['SITE'].length; i++) {


                    let PO = this.allEvents['SITE'][i];
                    sommeQtyProduced += this.allEvents['SITE'][i].qtyProduced * this.allEvents['SITE'][i].bottlesPerCase * 1;
                    sommeRejection += PO.fillerRejection * 1 + PO.caperRejection * 1 + PO.labelerRejection * 1 + PO.weightBoxRejection * 1;
                    fillerCounter += PO.fillerCounter * 1;
                    caperCounter += PO.caperCounter * 1;
                    labelerCounter += PO.labelerCounter * 1;
                    wieghtBoxCounter += PO.weightBoxCounter * 1;
                    qualityControlCounter += PO.qualityControlCounter * 1;

                }


                var summCompteur = 0;
                if (fillerCounter !== 0) {
                    summCompteur += (fillerCounter - sommeQtyProduced);
                }
                if (caperCounter !== 0) {
                    summCompteur += (caperCounter - sommeQtyProduced);
                }

                if (labelerCounter !== 0) {
                    summCompteur += (labelerCounter - sommeQtyProduced);
                }

                if (qualityControlCounter !== 0) {
                    summCompteur += (qualityControlCounter - sommeQtyProduced);
                }
                if (wieghtBoxCounter !== 0) {
                    summCompteur += (wieghtBoxCounter - sommeQtyProduced);
                }


                this.quality = (sommeQtyProduced) / (sommeQtyProduced + sommeRejection + summCompteur);

            },

            loadQualityLossesByFormat: function () {

                this.tableauFormats = [];
                this.qtyPerMachine = [];
                this.qtyPerMachine["caperCounter"] = [];
                this.qtyPerMachine["caperRejection"] = [];
                this.qtyPerMachine["fillerCounter"] = [];
                this.qtyPerMachine["fillerRejection"] = [];
                this.qtyPerMachine["weightBoxCounter"] = [];
                this.qtyPerMachine["weightBoxRejection"] = [];
                this.qtyPerMachine["labelerCounter"] = [];
                this.qtyPerMachine["labelerRejection"] = [];
                this.qtyPerMachine["controlQualityCounter"] = [];
                this.qtyPerMachine["controlQualityRejection"] = [];


                for (let i = 0; i < this.qualityLosses['formats'].length; i++) {

                    var line = this.qualityLosses['formats'][i];

                    var format = line.size;

                    if (!this.tableauFormats.includes(format)) {
                        this.tableauFormats.push(format);


                        this.qtyPerMachine["caperCounter"][format] = line.caperCounter;
                        this.qtyPerMachine["caperRejection"][format] = line.caperRejection;
                        this.qtyPerMachine["fillerCounter"][format] = line.fillerCounter;
                        this.qtyPerMachine["fillerRejection"][format] = line.fillerRejection;
                        this.qtyPerMachine["weightBoxCounter"][format] = line.weightBoxCounter;
                        this.qtyPerMachine["weightBoxRejection"][format] = line.weightBoxRejection;
                        this.qtyPerMachine["labelerCounter"][format] = line.labelerCounter;
                        this.qtyPerMachine["labelerRejection"][format] = line.labelerRejection;
                        this.qtyPerMachine["controlQualityCounter"][format] = line.qualityControlCounter;
                        this.qtyPerMachine["controlQualityRejection"][format] = line.qualityControlRejection;

                        this.totalPerFormat[format] = line.caperCounter + line.caperRejection + line.fillerCounter
                            + line.fillerRejection + line.weightBoxCounter + line.weightBoxRejection + line.labelerCounter +
                            line.labelerRejection + line.qualityControlCounter + line.qualityControlRejection;

                    } else {

                        this.qtyPerMachine["caperCounter"][format] += line.caperCounter;
                        this.qtyPerMachine["caperRejection"][format] += line.caperRejection;
                        this.qtyPerMachine["fillerCounter"][format] += line.fillerCounter;
                        this.qtyPerMachine["fillerRejection"][format] += line.fillerRejection;
                        this.qtyPerMachine["weightBoxCounter"][format] += line.weightBoxCounter;
                        this.qtyPerMachine["weightBoxRejection"][format] += line.weightBoxRejection;
                        this.qtyPerMachine["labelerCounter"][format] += line.labelerCounter;
                        this.qtyPerMachine["labelerRejection"][format] += line.labelerRejection;
                        this.qtyPerMachine["controlQualityCounter"][format] += line.qualityControlCounter;
                        this.qtyPerMachine["controlQualityRejection"][format] += line.qualityControlRejection;
                        this.totalPerFormat[format] += line.caperCounter + line.caperRejection + line.fillerCounter
                            + line.fillerRejection + line.weightBoxCounter + line.weightBoxRejection + line.labelerCounter +
                            line.labelerRejection + line.qualityControlCounter + line.qualityControlRejection;
                    }


                }

            },

            pieCharts: function () {
                let obj;
                var data = [];
                var totalPieChart1 = 0;
                for (let j = 0; j < this.qualityLossesPerMachineArray.length; j++) {
                    obj = {
                        name: this.qualityLossesPerMachineArray[j].name,
                        nbr: this.qualityLossesPerMachineArray[j].value
                    };
                    data.push(obj);
                    totalPieChart1 += this.qualityLossesPerMachineArray[j].value;
                }
                console.log('DATA');

                console.log(data);

                if (data.length === 0) {
                    obj = {
                        name: this.$t("nothingProduced"),
                        nbr: 1
                    };
                    data.push(obj);

                    totalPieChart1 = 1;
                }


                const randomHexColorCode = () => {
                    return "#" + Math.random().toString(16).slice(2, 8)
                };

                var canvas = document.getElementById("qualityLossesByMachines");
                var ctx = canvas.getContext("2d");
                canvas.width = 450;
                canvas.height = 350;
                let total = data.reduce((ttl, house) => {
                    return ttl + house.nbr
                }, 0);
                let startAngle = 0;
                let radius = 70;
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
                    var txt = this.$t(item.name) + '\n';
                    var pct = item.nbr / totalPieChart1 * 100;
                    txt = txt + ' ' + pct.toFixed(2) + '%';

                    ctx.fillText(txt, deltaX + cx, deltaY + cy);
                    ctx.closePath();

                    startAngle = endAngle;
                }

                data = [];
                var totalPieChart2 = 0;
                for (let j = 0; j < this.tableauFormats.length; j++) {
                    var format = this.tableauFormats[j];

                    obj = {
                        name: format,
                        nbr: this.totalPerFormat[format],
                    };
                    data.push(obj);
                    totalPieChart2 += this.totalPerFormat[format];
                }

                if (data.length === 0) {
                    obj = {
                        name: this.$t("nothingProduced"),
                        nbr: 1
                    };
                    data.push(obj);

                    totalPieChart2 = 1;
                }

                console.log('DATA');

                console.log(data);


                canvas = document.getElementById("qualityLossesByFormat");
                ctx = canvas.getContext("2d");
                canvas.width = 450;
                canvas.height = 350;
                total = data.reduce((ttl, house) => {
                    return ttl + house.nbr
                }, 0);
                startAngle = 0;
                radius = 70;
                cx = canvas.width / 2;
                cy = canvas.height / 2;

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

                    if (item.name !== this.$t("nothingProduced")) {
                        txt = item.name + 'L\n';
                        pct = item.nbr / totalPieChart2 * 100;
                        txt = txt + ' ' + pct.toFixed(2) + '%';
                    } else {
                        txt = item.name + '\n';
                    }


                    startAngle = endAngle;
                    ctx.fillText(txt, deltaX + cx, deltaY + cy);
                    ctx.closePath();

                    startAngle = endAngle;
                }


            },


        },

        mounted() {
            if (sessionStorage.getItem("language") !== null) {
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


            let externalScript = document.createElement('script');
            externalScript.setAttribute('src', 'https://canvasjs.com/assets/script/canvasjs.min.js');
            document.head.appendChild(externalScript)
        },

        computed: {
            ...mapGetters(['sites', 'speedLoss', 'allEvents', 'qualityLosses', 'allEvents'])
        },

        components: {
            ProductionWindow,
        }
    }
</script>
<style scoped>

    div {
        padding: 15px;
        background-color: #fff;

    }

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

    div.site-pl-selection > div {
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

    .content-title {
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
    }

    h1 {
        font-size: 1.4em;
        color: #56baed;
    }

    label {
        font-size: 1.4em;
        color: #56baed;
    }

    p {
        font-size: 1em;
        color: black;
    }

    h2 {
        font-size: 1.2em;
        color: #56baed;
    }

    h4 {
        color: red;
    }


    .row {
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
        max-height: 300px;
    }


    .mainLine {
        border: 2px solid black;
    }

    .boxWeigher {
        background: palegoldenrod;
    }

    .caper {
        background: palegreen;
    }

    .labeler {
        background: paleturquoise;
    }

    .filler {
        background: palevioletred;
    }

    .qualityControl {
        background: lightpink;
    }

    .totalQty {
        background: papayawhip;
    }

    .machineDiagram {
        background: lightblue;
        color: white;
        align: center;
    }

    h3 {
        align: center;
    }

    div.data {
        flex-direction: column;
        border: solid 1px;
        border-radius: 5px;
        padding: 10px 5px;
    }

    div.dataInput {
        border-bottom: solid 1px;

    }


</style>
