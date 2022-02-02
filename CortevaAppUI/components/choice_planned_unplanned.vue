<template>
    <div>
        <div align="center" class="productionName rcorners2">
            {{productionName}}
        </div>

        <br/>

        <div class="row" align="center">
            <div class="col">
                <button
                    class="btn btn-primary border-info btn-lg btn-block align-items-center btn-info"
                    type="button" @click.prevent="setDowntimeType('plannedDowntime')">
                    {{$t("plannedDowntime")}}
                </button>
            </div>

            <div class="col">

                <button
                    class="btn btn-primary border-info btn-lg btn-block align-items-center btn-info"
                    type="button" @click.prevent="setDowntimeType('unplannedDowntime')">
                    {{$t("unplannedDowntime")}}
                </button>

            </div>
        </div>
        <br/>


        <div align="center" class="rcorners2 table-info-data" id="summary">
            <table class="table">
                <thead>
                <tr>
                    <th scope="col">{{$t("type")}}</th>
                    <th scope="col">{{$t("entryTime")}}</th>
                    <th scope="col">{{$t("duration(Minutes)")}}</th>
                    <th scope="col">{{$t("comments")}}</th>

                </tr>
                </thead>
                <tbody>
                <template v-for="event in events1">
                    <tr>
                        <th scope="row">{{$t(event.type)}}</th>
                        <td>{{event.updated_at.split(' ')[1]}}</td>
                        <td>{{event.total_duration}}</td>
                        <td>{{event.comment}}</td>
                    </tr>
                </template>

                <template v-for="event in events2">
                    <tr>
                        <th scope="row">{{event.type}}</th>
                        <td>{{event.updated_at}}</td>
                        <td>{{event.total_duration}}</td>
                        <td>{{event.comment}}</td>
                    </tr>
                </template>
                </tbody>

            </table>
        </div>

        <br/>


        <div align="left">
            <button type="button" class="btn btn-danger" @click.prevent="backPage()">{{$t("back")}}</button>
        </div>
    </div>


</template>

<script>
    import {mapGetters} from "vuex";

    export default {
        name: "choice_planned_unplanned",

        data() {
            return {
                url: sessionStorage.getItem("url"),
                productionName: sessionStorage.getItem("productionName"),
                prodlines: sessionStorage.getItem("prodlines").split(','),
                indice: 0,
                productionlines: sessionStorage.getItem("prodlines").split(','),

                PO: sessionStorage.getItem("pos").split(','),


            }
        },

        methods: {

            setDowntimeType: function (downtimeType) {

                if (sessionStorage.getItem("downtimeType") === null) {
                    sessionStorage.downtimeType = downtimeType;
                } else {
                    sessionStorage.setItem("downtimeType", downtimeType);
                }

                window.location.href = this.url + 'summary/' + this.productionName + '/' + downtimeType;
            },

            backPage: function () {
                window.location.href = this.url + 'summary';
            }


        },

        mounted() {

            if(sessionStorage.getItem("language") !== null){
                this.$i18n.locale = sessionStorage.getItem("language");
            }

            console.log(this.prodlines);
            console.log(this.productionName);

            console.log('PO');


            console.log(this.PO);


            var tab = [];
            for (let i = 0; i < this.prodlines.length; i++) {
                if (this.productionName === this.prodlines[i]) {
                    this.indice = i;
                    tab.push(i + 1);
                    tab.push(this.PO[i]);
                    tab.push(this.prodlines[i]);
                    this.$store.dispatch('fetchEvents', tab);
                }
            }

            if (sessionStorage.getItem("indice") === null) {
                sessionStorage.indice = this.indice;
            } else {
                sessionStorage.setItem("indice", this.indice);
            }

        },

        computed: {
            ...mapGetters([
                'events1',
                'events2',
            ])
        }
    }
</script>

<style scoped>

    .productionName {
        left: 0;
        top: 0;
        max-width: 150px;
        margin-bottom: 50px;
    }

    .rcorners1 {
        border-radius: 25px;
        background: lightblue;
        padding: 20px;
        margin-bottom: 30px;
        width: 180px;

    }


    .rcorners2 {
        border-radius: 25px;
        border: 2px solid lightblue;
        padding: 20px;
    }

    #summary {
        padding: 40px;
    }

    button {
        color: white;
        margin-top: 20px;
    }

    .table-info-data {
        overflow:scroll; max-height: 300px;
    }

    thead {
        color:white;
        background: #56baed;
    }


</style>
