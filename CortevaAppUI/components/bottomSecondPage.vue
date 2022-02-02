<template>
    <div class="">
        <hr/>

        <br/>
        <div class="d-flex flex-row justify-content-between align-items-center bg-white"
             style="margin-left:auto; margin-right:auto;">
            <template v-for="productionline in productionlines">
                <label class="checkbox">
                    <input type="checkbox" class="check" v-on:click="setValue()">
                    <span id="endPO" class="response PO">{{$t("endPO")}}</span>
                </label>


                <label class="checkbox">
                    <input type="checkbox" class="check" v-on:click="setValue()">
                    <span id="endTeam" class="response team">{{$t("endTeam")}}</span>
                </label>

            </template>
        </div>

        <br/>
        <div align="center">
            <button type="button" class="btn btn-success" style="width : 50%;"
                    @click.prevent="validateDecision()">
                OK
            </button>
        </div>
        <div align="left">
            <button type="button" class="btn btn-danger" @click.prevent="backPage()">{{$t("back")}}</button>
        </div>
    </div>


</template>

<script>
    import {mapGetters} from "vuex";

    export default {
        name: "bottomSecondPage",


        data() {
            return {
                url: sessionStorage.getItem("url"),
                productionlines: sessionStorage.getItem("prodlines").split(','),

                pos: sessionStorage.getItem("pos"),

                PO: sessionStorage.getItem("pos").split(','),


            }
        },


        methods: {
            setValue: function () {
                var elements = document.getElementsByClassName("response");
                var checkBoxes = document.getElementsByClassName("check");

                for (let i = 0; i < elements.length; i++) {
                    if (checkBoxes[i].checked) {
                        console.log(i);
                        elements[i].innerHTML = this.$t("yes");
                    } else {
                        if (i % 2 === 0) {
                            elements[i].innerHTML = this.$t("endPO");
                        } else {
                            elements[i].innerHTML = this.$t("endTeam");

                        }
                    }
                }

            },

            backPage: function () {
                window.location.href = this.url + 'teamInfo';
            },

            validateDecision: function () {

                var indice = 0;
                var nbSelected = 0;
                var elements = document.getElementsByClassName("response");
                console.log(elements);

                for (let i = 0; i < elements.length; i++) {
                    if (elements[i].innerHTML === this.$t("yes")) {
                        indice = i;
                        nbSelected++;
                    }
                }

                if (nbSelected === 1) {
                    var balise = elements[indice];
                    if (balise.id === "endPO") {
                        console.log(indice);


                        console.log(this.productionlines[indice / this.productionlines.length]);

                        if (sessionStorage.getItem("productionName") === null) {
                            sessionStorage.productionName = this.productionlines[indice / this.productionlines.length];
                        } else {
                            sessionStorage.setItem("productionName", this.productionlines[indice / this.productionlines.length]);
                        }

                        if (sessionStorage.getItem("GMIDCODE") === null) {
                            sessionStorage.GMIDCODE = sessionStorage.GMID.split(',')[indice / this.productionlines.length];
                        } else {
                            sessionStorage.setItem("GMIDCODE", sessionStorage.GMID.split(',')[indice / this.productionlines.length]);
                        }

                        console.log('events1 : ' + this.productionlines[indice / this.productionlines.length]);

                        var sommePlannedEvents = 0;
                        var sommeUnplannedEvents = 0;


                        for (let i = 0; i < this.events1.length; i++) {
                            //console.log(this.events1[i]);

                            if (this.events1[i].productionline === this.productionlines[indice / this.productionlines.length]) {
                                console.log('events1');

                                console.log(this.events1[i]);

                                if (this.events1[i].kind === 0) {
                                    sommePlannedEvents += this.events1[i].total_duration;
                                } else {
                                    sommeUnplannedEvents += this.events1[i].total_duration;
                                }

                            }
                        }

                        for (let i = 0; i < this.events2.length; i++) {
                            if (this.events2[i].productionline === this.productionlines[indice / this.productionlines.length]) {


                                if (this.events2[i].kind === 0) {
                                    sommePlannedEvents += this.events2[i].total_duration;
                                } else {
                                    sommeUnplannedEvents += this.events2[i].total_duration;
                                }
                            }
                        }


                        //console.log(this.events1);

                        console.log('UNPLANNED SOMME : ' + sommeUnplannedEvents);
                        console.log('PLANNED SOMME : ' + sommePlannedEvents);


                        if (sessionStorage.getItem("sommeUnplannedEvents") === null) {
                            sessionStorage.sommeUnplannedEvents = sommeUnplannedEvents;
                        } else {
                            sessionStorage.setItem("sommeUnplannedEvents", sommeUnplannedEvents);
                        }

                        if (sessionStorage.getItem("sommePlannedEvents") === null) {
                            sessionStorage.sommePlannedEvents = sommePlannedEvents;
                        } else {
                            sessionStorage.setItem("sommePlannedEvents", sommePlannedEvents);
                        }


                        window.location.href = this.url + 'endPO/' + this.productionlines[indice / this.productionlines.length] + '/endPO';


                    } else {


                        sessionStorage.setItem("crewLeader", '');
                        sessionStorage.setItem("typeTeam", '');
                        sessionStorage.setItem("workingEnd", '');
                        sessionStorage.setItem("workingDebut", '');
                        sessionStorage.setItem("site", '');


                        window.location.href = this.url + 'teamInfo';


                    }
                }
            }
        },

        mounted() {

            if(sessionStorage.getItem("language") !== null){
                this.$i18n.locale = sessionStorage.getItem("language");
            }
            console.log("POS : ");

            console.log(sessionStorage.getItem("pos").split(','));

            if (this.PO.length > 0 && this.PO[0] !== "") {
                for (let i = 0; i < this.PO.length; i++) {
                    var tab = [];
                    tab.push(i + 1);
                    tab.push(this.PO[i]);
                    tab.push(this.productionlines[i]);
                    this.$store.dispatch('fetchEvents', tab);
                }

            }



            console.log(sessionStorage.getItem("GMID").split(','));
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

    hr {
        color: #ffae42;
        background-color: #ffae42;
        border: none;
        height: 2px;
        width: 50%;
        alignment: center;
    }

    label.checkbox {
        cursor: pointer
    }

    label.checkbox input {
        position: absolute;
        top: 0;
        left: 0;
        visibility: hidden;
        pointer-events: none
    }

    label.checkbox span {
        padding: 4px 0px;
        border: 1px solid #ffae42;
        display: inline-block;
        color: #ffae42;
        width: 100px;
        text-align: center;
        border-radius: 3px;
        margin-top: 7px;
        text-transform: uppercase
    }

    label.checkbox input:checked + span {
        border-color: #ffae42;
        background-color: #ffae42;
        color: #fff
    }


    .btn:focus {
        outline: 0 !important;
        box-shadow: none !important
    }

    .btn:active {
        outline: 0 !important;
        box-shadow: none !important
    }

    #ligne {
        margin-left: auto;
        margin-right: auto;
    }

</style>
