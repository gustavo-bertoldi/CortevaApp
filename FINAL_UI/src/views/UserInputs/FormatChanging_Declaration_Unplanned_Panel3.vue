<template>
  <div class="home">
    <navbar-saisie></navbar-saisie>
    <ChangingFormat_Declaration_Unplanne_Pannel_3></ChangingFormat_Declaration_Unplanne_Pannel_3>
  </div>
</template>

<script>
import NavbarSaisie from "@/components/UserInputComponents/NavbarSaisie";
import ChangingFormat_Declaration_Unplanne_Pannel_3 from "@/components/UserInputComponents/ChangingFormat_Declaration_Unplanned_Pannel_3";

export default {
  name: "FormatChanging_Declaration_Unplanned_Panel3",

  components: {
    ChangingFormat_Declaration_Unplanne_Pannel_3,
    NavbarSaisie,
  }
}
</script>

<style scoped>

</style>