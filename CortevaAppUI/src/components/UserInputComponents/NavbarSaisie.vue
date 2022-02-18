<template>
  <div class="nv">
    <nav class="navbar">
      <img src="../../assets/icon.png" alt="" width="150" height="100">

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
  name: "NavbarSaisie",
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

</style>