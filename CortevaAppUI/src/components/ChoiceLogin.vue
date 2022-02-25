<template>
  <div class="container">
    <br>
    <div class="row">
      <div class="col">
        <router-link to="/login">
          <button class="btn btn-primary">{{ $t("choiceLoginOperator") }}</button>
        </router-link>
      </div>

      <div class="col">
        <router-link to="/login">
          <button  class="btn btn-primary">{{ $t("choiceLoginSupervisor") }}
          </button>
        </router-link>
      </div>

      <div class="col">
        <router-link to="/login">
          <button class="btn btn-primary">{{ $t("choiceLoginAdministrator") }}
          </button>
        </router-link>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "ChoiceLogin",
  methods: {
    /**

    displayLoginPage: function (loginPage) {

      if (sessionStorage.getItem("loginType") === null) {
        sessionStorage.loginType = loginPage;
      } else {
        sessionStorage.setItem("loginType", loginPage);
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

      } */
    },

}
</script>

<style scoped>

</style>