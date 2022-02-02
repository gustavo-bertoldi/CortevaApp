<template>
    <div class="nv">

        <nav class="navbar">
            <!--<a class="navbar-brand" v-bind:href="url + 'packagingLineID' ">-->
                <img src="images/icon.png" alt="" width="150" height="100">
            <!--</a>-->

            <div class="d-flex">

                <select name="languages" id="languages" v-model="language" v-on:change="swapLanguage(language)">
                    <option value="fr">
                        Fr
                    </option>
                    <option value="en" >
                        En
                    </option>
                </select>

                <select name="page" id="page" class="" v-model="selection">
                    <option value="packagingLineID">
                        {{$t("packagingLineID")}}
                    </option>

                    <option value="downtimesReport">
                        {{$t("downtimesReport")}}
                    </option>

                    <option value="qualityLossesDashboard">
                        {{$t("qualityLossesDashboard")}}
                    </option>

                    <option value="productionDashboard">
                        {{$t("productionDashboard")}}
                    </option>

                    <option value="overallLineEffectivness">
                        Overall Line Effectivness
                    </option>

                    <option value="unplannedDowntimeDashboard">
                        Unplanned Downtime Dashboard
                    </option>

                    <option value="unplannedDowntimeShutdowns">
                        Unplanned Downtime Shutdowns
                    </option>

                    <option value="unplannedDowntimeSpeedLosses">
                        Unplanned Downtime Speed Losses
                    </option>


                </select>

                <button class="btn btn-outline-success" type="button" v-on:click="nextPage()">
                    {{$t("open")}}
                </button>

            </div>

        </nav>


    </div>

</template>

<script>
    export default {
        name: "navbar",

        data() {
            return {
                selection: 'packagingLineID',
                url: sessionStorage.getItem("url"),
                language: "en",

            }
        },

        methods: {
            nextPage: function () {
                console.log(this.url + this.selection);
                window.location.href = this.url + this.selection;
            },

            swapLanguage : function(language)
            {
                this.$i18n.locale = language;
                if(sessionStorage.getItem("language") === null){
                    sessionStorage.language = language;
                }else{
                    sessionStorage.setItem("language",language);
                }
            },

            mounted: function()
            {
                if(sessionStorage.getItem("language") === null){
                    this.language = "en";
                }else{
                    this.language = sessionStorage.getItem("language");
                }
            }
        },

    }
</script>

<style scoped>

    .nv {
        background: lightblue;
        width: 100%;

    }
</style>
