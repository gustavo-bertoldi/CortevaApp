<template>
 <div class="">
   <navbar-saisie></navbar-saisie>
   <PlannedEvent_Pannel3></PlannedEvent_Pannel3>
 </div>
</template>

<script>
import NavbarSaisie from "@/components/UserInputComponents/NavbarSaisie";
import PlannedEvent_Pannel3 from "@/components/UserInputComponents/PlannedEvent_Pannel3";


export default {
  name: "IncidentDeclaration",
  components: {
    NavbarSaisie,
    PlannedEvent_Pannel3
  }
}
</script>

<style scoped>

</style>