<template>
  <div class="home">
    <navbar-saisie></navbar-saisie>
    <choice-login></choice-login>
  </div>
</template>

<script>
import NavbarSaisie from "@/components/NavbarSaisie";
import ChoiceLogin from "@/components/ChoiceLogin";

export default {
  name: "ChoiceLoginPage",
  components: {
    ChoiceLogin,
    NavbarSaisie,
  }
}
</script>

<style scoped>

</style>