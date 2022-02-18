<template>
<div>
  <Navbar></Navbar>
  <UnplannedSpeedlosses></UnplannedSpeedlosses>
</div>
</template>

<script>
import Navbar from "@/components/DashboardComponents/Navbar";
import UnplannedSpeedlosses from "@/components/DashboardComponents/UnplannedSpeedlosses";

export default {
  name: "UnplannedSpeedlosses_Dashboard",
  components: {
    Navbar,
    UnplannedSpeedlosses,
  }
}
</script>

<style scoped>

</style>