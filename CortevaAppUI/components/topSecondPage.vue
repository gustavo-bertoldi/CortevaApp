<template>


        <div class="container">

            <!--{{assignation}}-->
            <!-- {{events1}}
             {{events2}}-->
            <div class="row">
                <template v-for="productionline in productionlines">
                    <div class="col">
                        <button
                            class="btn btn-primary border-info btn-lg btn-block align-items-center btn-info rcorners1"
                            type="button" @click.prevent="setProductionline(productionline)">
                            {{productionline}}
                        </button>


                        <br/>
                        <div class="rcorners2">
                            <p>
                                {{$t("site")}} : {{site}} <br/>
                                {{$t("crewLeader")}} : {{crewLeader}} <br/>
                                {{$t("typeTeam")}} : {{typeTeam}} <br/>
                                {{$t("startTime")}} : {{workingDebut}} <br/>
                                {{$t("endTime")}} : {{workingEnd}} <br/>
                            </p>
                        </div>

                        <br/>

                        <div class="rcorners1 table-info-data" align="center">
                            <!--
                                        <template v-if="events1.length ===0 && events2.length ===0">

                                            <h4 align="center">AUCUN INCIDENT DECLARE</h4>

                                        </template>
                                        -->
                            <table class="table">
                                <thead class="thead-dark">
                                <tr>
                                    <th scope="col">{{$t("type")}}</th>
                                    <th scope="col">{{$t("entryTime")}}</th>
                                    <th scope="col">{{$t("duration(Minutes)")}}</th>
                                    <th scope="col">{{$t("comments")}}</th>

                                </tr>
                                </thead>
                                <tbody>
                                <template v-for="event in events1">
                                    <template v-if="event.productionline === productionline">
                                        <tr>
                                            <th scope="row">{{$t(event.type)}}</th>
                                            <td>{{event.updated_at.split(' ')[1]}}</td>
                                            <td>{{event.total_duration}}</td>
                                            <td>{{event.comment}}</td>
                                        </tr>
                                    </template>
                                </template>

                                <template v-for="event in events2">
                                    <template v-if="event.productionline === productionline">
                                        <tr>
                                            <th scope="row">{{$t(event.type)}}</th>
                                            <td>{{event.updated_at.split(' ')[1]}}</td>
                                            <td>{{event.total_duration}}</td>
                                            <td>{{event.comment}}</td>
                                        </tr>
                                    </template>
                                </template>
                                </tbody>

                            </table>


                            <!--{{event.type}}    {{event.predicted_duration}}   {{event.total_duration}}-->
                        </div>

                        <br/>

                    </div>

                </template>
                <!--
                 <div class="col">
                     <a v-bind:href="url + 'summary/' +  productionB"  id="productionB" class="link-dark">

                         <div class="rcorners1"  align="center">
                             {{productionB}}

                         </div>
                     </a>

                     <br/>

                     <div class="rcorners2">
                         <p>
                             Site : {{site}} <br/>
                             Crew Leader : {{crewLeader}} <br/>
                             Type : {{typeTeam}} <br/>
                             Début : {{workingDebut}} <br/>
                             Fin : {{workingEnd}} <br/>
                         </p>
                     </div>

                     <div class="rcorners1">
                         <p>
                             Rapport de production en cours
                         </p>
                     </div>

                 </div>

         -->

            </div>

            <br/>

        </div>
</template>

<script>


    import {mapGetters} from "vuex";

    export default {
        name: "topSecondPage",

        data() {
            return {
                site: sessionStorage.getItem("site"),
                workingDebut: sessionStorage.getItem("workingDebut"),
                workingEnd: sessionStorage.getItem("workingEnd"),
                productionlines: sessionStorage.getItem("prodlines").split(','),
                username: sessionStorage.getItem("username"),
                PO: sessionStorage.getItem("pos").split(','),
                GMID: sessionStorage.getItem("GMID").split(','),

                //productionA :  sessionStorage.getItem("productionA"),

                //productionB :  sessionStorage.getItem("productionB"),
                crewLeader: sessionStorage.getItem("crewLeader"),
                typeTeam: sessionStorage.getItem("typeTeam"),
                url: sessionStorage.getItem("url"),

                showSpinner: 0,


            };
        },

        methods: {


            setProductionline: function (productionline) {

                if (sessionStorage.getItem("productionName") === null) {
                    sessionStorage.productionName = productionline;
                } else {
                    sessionStorage.setItem("productionName", productionline);
                }


                window.location.href = this.url + 'summary/' + productionline;

            },

            resolveAfter05Second: function () {
                return new Promise(resolve => {
                    setTimeout(() => {
                        resolve('resolved');
                    }, 1000);
                });


            },

            resolveAfter15Second: function () {
                return new Promise(resolve => {
                    setTimeout(() => {
                        resolve('resolved');
                    }, 1000);
                });


            },
            /**
             ajouterSessionProductionNameA : function(){

                if(sessionStorage.getItem("productionName") === null){
                    sessionStorage.productionName = this.productionA;
                }else{
                    sessionStorage.setItem("productionName", this.productionA);
                }
                window.location.href = this.url + 'summary/' + this.productionA;

                return false;
            },

             ajouterSessionProductionNameB : function(){

                if(sessionStorage.getItem("productionName") === null){
                    sessionStorage.productionName = this.productionB;
                }else{
                    sessionStorage.setItem("productionName", this.productionB);
                }

                window.location.href = this.url + 'summary/' + this.productionB;

                return false;
            }
             **/


        },

        async mounted() {
            if(sessionStorage.getItem("language") !== null){
                this.$i18n.locale = sessionStorage.getItem("language");
            }

            this.$store.dispatch('fetchSites');



            console.log("POS : ");

            console.log(sessionStorage.getItem("pos").split(','));

            await this.$store.dispatch('getWorksiteID', sessionStorage.getItem("site"));
            await this.resolveAfter05Second();

            var POElement = [];

            console.log(this.PO);
            if (this.PO.length > 0 && this.PO[0] !== "") {
                for (let i = 0; i < this.PO.length; i++) {
                    var tab = [];
                    tab.push(i + 1);
                    tab.push(this.PO[i]);
                    tab.push(this.productionlines[i]);
                    await this.$store.dispatch('fetchEvents', tab);


                    await this.$store.dispatch('getProductionlineID', this.productionlines[i]);
                    await this.resolveAfter15Second();

                    var assignation = {
                        username: this.username,
                        productionline: this.productionlineID[0].id,
                        po: this.PO[i],
                        shift: sessionStorage.getItem("typeTeam"),
                        worksite: this.worksiteID[0].id,
                    };

                    await this.$store.dispatch('checkAssignation', assignation);
                    await this.resolveAfter05Second();

                    if (this.assignation[i] === 0) {
                        await this.$store.dispatch('storeAssignation', assignation);
                    }

                    /**
                     var po = this.PO[i];
                     await this.$store.dispatch('checkPO', po);
                     await this.resolveAfter05Second();

                     if (this.checkPO === 0) {
                        let element = {
                            number: po,
                            GMIDCode: this.GMID[i],
                            productionline_name: this.productionlines[i],

                        };

                        POElement.push(element);

                    }
                     **/

                }

                //this.$store.dispatch('create_PO', POElement);

            }

            this.showSpinner =1;

        },

        computed: {
            ...mapGetters([
                'events1',
                'events2',
                'productionlineID',
                'worksiteID',
                'assignation',
                'checkPO',
            ])
        }

    }
</script>

<style scoped>

    .rcorners1 {
        border-radius: 25px;
        background: lightblue;
        padding-top: 20px;
        padding-left: 20px;
        padding-right: 20px;
        padding-bottom: 10px;

    }

    .rcorners2 {
        border-radius: 25px;
        border: 2px solid lightblue;
        padding: 20px;

    }

    .table-info-data {
        overflow: scroll;
        max-height: 300px;
    }

    thead {
        color: white;
        background: #56baed;
    }
</style>
