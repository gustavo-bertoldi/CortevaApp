<template>
    <div>
        <div align="center" class="productionName rcorners2">

            {{title}}

        </div>

        <br/>

        <form id="form">


            <div class="form-group row">
                <label class="col-sm-2 col-form-label rcorners1" for="expectedDuration">{{$t("expectedDuration(Minutes)")}}</label>
                <div class="col-sm-10" id="expectedDuration">
                    <div class="row" align="center" style="margin-bottom: 30px;">

                        <div class="col-sm-4">
                            <label for="answer1" class="checkbox labelsAnswer">
                                <input type="radio"
                                       id="answer1" name="reponseQuestion"
                                       value="5" class="response">
                                <span>5</span>
                            </label>

                        </div>

                        <div class="col-sm-4">
                            <label for="answer2" class="checkbox labelsAnswer">
                                <input type="radio"
                                       id="answer2" name="reponseQuestion"
                                       value="15" class="response">
                                <span>15</span>
                            </label>


                        </div>

                        <div class="col-sm-4">

                            <label for="answer3" class="checkbox labelsAnswer">
                                <input type="radio"
                                       id="answer3" name="reponseQuestion"
                                       value="30" class="response">
                                <span>30</span>
                            </label>


                        </div>

                        <div class="col-sm-4">

                            <label for="answer5" class="checkbox labelsAnswer">
                                <input type="radio"
                                       id="answer5" name="reponseQuestion"
                                       value="45" class="response">
                                <span>45</span>
                            </label>


                        </div>

                        <div class="col-sm-4">
                            <label for="answer4" class="checkbox labelsAnswer">
                                <input type="radio"
                                       id="answer4" name="reponseQuestion"
                                       value="60" class="response">
                                <span>60</span>
                            </label>

                        </div>

                    </div>
                    <!--<input type="number" id="expectedDuration" class="form-control-plaintext rcorners2" v-bind:value="ChangingFormat_Event.predicted_duration" disabled>-->
                </div>
            </div>


            <div class="form-group row">
                <label class="col-sm-2 col-form-label rcorners1" for="totalDuration">{{$t("totalDuration(Minutes)")}}</label>
                <div class="col-sm-10">
                    <input type="number" id="totalDuration" class="form-control-plaintext rcorners2">
                </div>
            </div>

            <div class="form-group row">
                <label for="comments" class="col-sm-2 rcorners1">{{$t("comments")}}</label>
                <div class="col-sm-10">
                    <textarea id="comments" class="rcorners2"></textarea>
                </div>
            </div>


            <div align="right">
                <button class="btn btn-primary border-danger align-items-right btn-danger" type="button"
                        @click.prevent="backOrigin()">
                    {{$t("cancel")}}
                </button>
            </div>
            <div class="d-flex flex-row justify-content-between align-items-center bg-white">
                <button class="btn btn-primary d-flex align-items-center btn-danger" type="button"
                        @click.prevent="backPage()">
                    {{$t("back")}}
                </button>

                <button class="btn btn-primary border-success align-items-center btn-success" type="button"
                        @click.prevent="validateInformations()">
                    OK
                </button>
            </div>
        </form>


    </div>

</template>

<script>
    export default {
        name: "Changingformat_Declaration",

        data() {
            return {
                url: sessionStorage.getItem("url"),
                productionName: sessionStorage.getItem("productionName"),
                downtimeType: sessionStorage.getItem("downtimeType"),

                parameters: [],
                printedStep: 0,
                title: 'Changement de format',

                indice: sessionStorage.getItem("indice"),

                ChangingFormat_Event: {

                    productionline : sessionStorage.getItem("productionName"),
                    predicted_duration: null,
                    total_duration: null,
                    comment: '',
                    OLE:  '',

                },


            }
        },

        methods: {

            validateInformations: function () {

                var responses = document.getElementsByClassName('response');
                var duration = 0;
                for (let i = 0; i < responses.length; i++) {
                    if (responses[i].checked) {
                        duration = responses[i].value;
                    }
                }

                this.ChangingFormat_Event.predicted_duration = duration;
                this.ChangingFormat_Event.total_duration = document.getElementById('totalDuration').value;
                this.ChangingFormat_Event.comment = document.getElementById('comments').value;
                this.ChangingFormat_Event.OLE = sessionStorage.getItem("pos").split(',')[this.indice];

                if(this.ChangingFormat_Event.predicted_duration > 0 &&this.ChangingFormat_Event.total_duration > 0){
                    this.$store.dispatch('create_UnplannedEvent_Changingformat', this.ChangingFormat_Event);
                    //this.backOrigin();

                }else{
                    this.errorMessage();
                }

                console.log(this.ChangingFormat_Event);



                //this.$store.dispatch('create_UnplannedEvent_UnplannedDowntime', this.unplannedEvent);


            },
            errorMessage : function(){
                var h1 = document.getElementsByClassName("error");
                if(h1.length <= 0){
                    let error = document.createElement('h1');
                    error.setAttribute("class", "error");
                    error.innerHTML = this.$t("errorInput");
                    error.setAttribute("style", "color:red;")
                    error.setAttribute("align", "center");
                    let br = document.createElement('br');

                    let form = document.getElementById("form");
                    form.insertBefore(br, form.firstChild);
                    form.insertBefore(error, form.firstChild);
                }

            },

            backOrigin: function () {

                window.location.href = this.url + 'summary';


            },


            resolveAfter05Second: function () {
                return new Promise(resolve => {
                    setTimeout(() => {
                        resolve('resolved');
                    }, 500);
                });
            },

            backPage: function () {


                window.location.href = this.url + 'summary/' + this.productionName + '/' + this.downtimeType;


            }


        },

        mounted() {

            if(sessionStorage.getItem("language") !== null){
                this.$i18n.locale = sessionStorage.getItem("language");
            }
        },


    }
</script>


<style scoped>


    .productionName {
        left: 0;
        top: 0;
        min-width: 150px;
        max-width: 250px;

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

    .wrapper {
        display: grid;
        grid-template-columns: 1fr 1fr 1fr;
    }


    button {
        color: white;
        margin-top: 20px;


    }

    input {
        width: 40%;

    }


    #comments {
        height: 150px;
        width: 70%;
        border-radius: 25px;
        border: 2px solid lightblue;
        padding: 20px;

    }

    form {
        display: block;
        margin-left: auto;
        margin-right: auto;
    }

    label.checkbox {
        cursor: pointer;
    }

    label.checkbox input {
        position: absolute;
        top: 0;
        left: 0;
        visibility: hidden;
        pointer-events: none;

    }

    label.labelError {
        cursor: pointer;
    }

    label.labelError input {
        position: absolute;
        top: 0;
        left: 0;
        visibility: hidden;
        pointer-events: none;

    }




    label.checkbox span {
        padding: 4px 0px;
        border: 1px solid #56baed;
        display: inline-block;
        color: #56baed;
        width: 200px;
        text-align: center;
        border-radius: 3px;
        margin-top: 7px;
        text-transform: uppercase
    }

    label.checkbox input:checked + span {
        border-color: #56baed;
        background-color: #56baed;
        color: #fff
    }
    thead {
        color:white;
        background: #56baed;
    }

</style>
