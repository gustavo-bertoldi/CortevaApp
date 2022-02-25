<template>
  <div class="home">
    <navbar-saisie></navbar-saisie>
    <CIP_Declaration_Unplanned_Pannel_3></CIP_Declaration_Unplanned_Pannel_3>
  </div>
</template>

<script>
import NavbarSaisie from "@/components/UserInputComponents/NavbarSaisie";
import CIP_Declaration_Unplanned_Pannel_3 from "@/components/UserInputComponents/CIP_Declaration_Unplanned_Pannel_3";

export default {
  name: "CIP_Declaration_Unplanned_Panel3",
  components: {
    CIP_Declaration_Unplanned_Pannel_3,
    NavbarSaisie,
  }
}
</script>

<style scoped>

</style>