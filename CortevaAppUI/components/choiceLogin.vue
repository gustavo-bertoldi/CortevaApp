<template>

    <div class="container">
        <br>
        <div class="row">
            <div class="col">
                <button v-on:click="displayLoginPage('op')" class="btn btn-primary">{{ $t("choiceLoginOperator") }}
                </button>
            </div>

            <div class="col">
                <button v-on:click="displayLoginPage('sup')" class="btn btn-primary">{{ $t("choiceLoginSupervisor") }}
                </button>
            </div>

            <div class="col">
                <button v-on:click="displayLoginPage('admin')" class="btn btn-primary">{{ $t("choiceLoginAdministrator")
                    }}
                </button>
            </div>
        </div>
    </div>

</template>

<script>

    export default {
        name: "choiceLogin",
        data() {
            return {}
        },
        methods: {
            displayLoginPage: function (loginPage) {

                if (sessionStorage.getItem("loginType") === null) {
                    sessionStorage.loginType = loginPage;
                } else {
                    sessionStorage.setItem("loginType", loginPage);
                }

                if (sessionStorage.getItem("url") === null) {
                    sessionStorage.url = 'http://127.0.0.1:8000/';
                }

                let currentLink = window.location.href;

                switch (loginPage) {
                    case 'op' :
                        window.location.href = currentLink + 'login/operator';
                        break;

                    case 'admin' :
                        window.location.href = currentLink + 'login/administrator';
                        break;

                    case 'sup' :
                        window.location.href = currentLink + 'login/supervisor';
                        break;

                }
            },

            mounted() {
                if(sessionStorage.getItem("language") !== null){
                    this.$i18n.locale = sessionStorage.getItem("language");
                }
            },

        },
        computed: {

        }
    }
</script>

<style scoped>

</style>
