<template>
    <div class="nv">

        <nav class="navbar">
            <img src="img/icon.png" alt="" width="150" height="100">

            <div class="d-flex">

                <select name="languages" id="languages" v-model="language" v-on:change="swapLanguage(language)">
                    <option value="fr">
                        Fr
                    </option>
                    <option value="en" >
                        En
                    </option>
                </select>
            </div>

        </nav>


    </div>

</template>

<script>
    export default {
        name: "navbarSaisie",

        data() {
            return {
                url: sessionStorage.getItem("url"),
                language: "en",
            }
        },

        methods: {

            swapLanguage: function (language) {
                this.$i18n.locale = language;
                if(sessionStorage.getItem("language") === null){
                    sessionStorage.language = language;
                }else{
                    sessionStorage.setItem("language",language);
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
