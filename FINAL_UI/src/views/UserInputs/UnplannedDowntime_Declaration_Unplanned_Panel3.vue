<template>
  <div>
    <navbar-saisie></navbar-saisie>
    <UnplannedEvent_Declaration_Unplanned_Pannel_3></UnplannedEvent_Declaration_Unplanned_Pannel_3>

  </div>
</template>


<script>
import NavbarSaisie from "@/components/UserInputComponents/NavbarSaisie";
import UnplannedEvent_Declaration_Unplanned_Pannel_3 from "@/components/UserInputComponents/UnplannedEvent_Declaration_Unplanned_Pannel_3";

export default {
  name: "UnplannedDowntime_Declaration_Unplanned_Panel3",
  components: {
    UnplannedEvent_Declaration_Unplanned_Pannel_3,
    NavbarSaisie,
}
}
</script>

<style scoped>

</style>