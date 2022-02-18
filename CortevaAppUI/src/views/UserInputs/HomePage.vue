<template>
  <div class="home">
    <navbar-saisie></navbar-saisie>
    <top-home-page-user></top-home-page-user>
    <bottom-home-page-user></bottom-home-page-user>
  </div>


</template>

<script>
import TopHomePageUser from "@/components/UserInputComponents/TopHomePageUser";
import NavbarSaisie from "@/components/UserInputComponents/NavbarSaisie";
import BottomHomePageUser from "@/components/UserInputComponents/BottomHomePageUser";

export default {
  name: "HomePage",
  components: {
    NavbarSaisie,
    TopHomePageUser,
    BottomHomePageUser
  }
}
</script>

<style scoped>

</style>