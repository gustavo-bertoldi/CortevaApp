<template>


</template>


<!--<div class="row">

                <div class="col">

                    <div class="row">
                        <input type="text" class="form-control-plaintext rcorners2" value="Compteur" disabled>

                    </div>

                    <div class="row">
                        <input type="text" class="form-control-plaintext rcorners2" value="Compteur" disabled>
                    </div>

                    <div class="row">
                        <div class="">
                            <input type="text" class="form-control-plaintext rcorners2" value="Rejection" disabled
                                   >
                        </div>
                    </div>

                </div>

                <div class="col">

                    <div class="row">
                        <input type="text" id="Filler" class="form-control-plaintext rcorners2" value="Remplisseuse"
                               disabled>

                    </div>

                    <div class="row">
                        <input type="number" id="FillerCounter" class="form-control-plaintext rcorners2">
                    </div>

                    <div class="row">
                        <input type="number" id="FillerRejection" class="form-control-plaintext rcorners2">

                    </div>
                </div>

                <div class="col">

                    <div class="row">
                        <input type="text" id="Caper" class="form-control-plaintext rcorners2" value="Caper"
                               disabled>
                    </div>

                    <div class="row">
                        <input type="number" id="CaperCounter" class="form-control-plaintext rcorners2">
                    </div>

                    <div class="row">
                        <input type="number" id="CaperRejection" class="form-control-plaintext rcorners2">

                    </div>
                </div>

                <div class="col">

                    <div class="row">
                        <input type="text" id="Etiqueteuse" class="form-control-plaintext rcorners2"
                               value="Etiqueteuse"
                               disabled>
                    </div>

                    <div class="row">
                        <input type="number" id="EtiqueteuseCounter" class="form-control-plaintext rcorners2">
                    </div>

                    <div class="row">
                        <input type="number" id="EtiqueteuseRejection" class="form-control-plaintext rcorners2">

                    </div>
                </div>

                <div class="col">

                    <div class="row">
                        <input type="text" id="WieghtBox" class="form-control-plaintext rcorners2"
                               value="Poids des boites"
                               disabled>
                    </div>

                    <div class="row">
                        <input type="number" id="WieghtBoxCounter" class="form-control-plaintext rcorners2">
                    </div>

                    <div class="row">
                        <input type="number" id="WieghtBoxRejection" class="form-control-plaintext rcorners2">

                    </div>
                </div>
            </div>-->

<script>
    export default {
        name: "qualityDeclaration",

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
        padding: 10px;
    }


</style>
