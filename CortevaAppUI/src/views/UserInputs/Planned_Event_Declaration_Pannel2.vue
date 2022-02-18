<template>
<div>
  <navbar-saisie></navbar-saisie>
  <PlannedEvent_Pannel2></PlannedEvent_Pannel2>
</div>
</template>

<script>
import NavbarSaisie from "@/components/UserInputComponents/NavbarSaisie";
import PlannedEvent_Pannel2 from "@/components/UserInputComponents/Planned_Unplanned_Pannel2_Choice_Reason";

export default {
  name: "Planned_Event_Declaration_Pannel2",
  components: {
    NavbarSaisie,
    PlannedEvent_Pannel2
  }
}
</script>

<style scoped>

</style>