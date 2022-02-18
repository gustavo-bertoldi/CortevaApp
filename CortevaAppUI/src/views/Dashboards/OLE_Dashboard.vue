<template>
<div>
  <Navbar></Navbar>
  <OLE></OLE>
</div>
</template>

<script>
import Navbar from "@/components/DashboardComponents/Navbar";
import OLE from "@/components/DashboardComponents/OLE";

export default {
  name: "OLE_Dashboard",
  components: {
    Navbar,
    OLE,
  }
}
</script>

<style scoped>

</style>