<template>
<div>
  <NavbarSaisie></NavbarSaisie>
  <EndPO_Declaration></EndPO_Declaration>
</div>
</template>

<script>
import EndPO_Declaration from "@/components/UserInputComponents/EndPO_Declaration";
import NavbarSaisie from "@/components/UserInputComponents/NavbarSaisie";
export default {
  name: "End_PO_Declaration",
  components: {
    EndPO_Declaration,
    NavbarSaisie,
  }
}
</script>

<style scoped>

</style>