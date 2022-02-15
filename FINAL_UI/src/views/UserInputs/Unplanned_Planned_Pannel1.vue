<template>
  <div class="">
    <navbar-saisie></navbar-saisie>
    <ChoiceUnplannedPlanned></ChoiceUnplannedPlanned>
  </div>
</template>

<script>
import NavbarSaisie from "@/components/UserInputComponents/NavbarSaisie";
import ChoiceUnplannedPlanned from "@/components/UserInputComponents/ChoicePlannedUnplannedEvent";

export default {
  name: "Unplanned_Planned_Pannel1",
  components: {
    NavbarSaisie,
    ChoiceUnplannedPlanned
  }
}
</script>

<style scoped>

</style>