<template>
  <div>
    <Navbar></Navbar>
    <UnplannedDowntimes></UnplannedDowntimes>
  </div>

</template>

<script>
import Navbar from "@/components/DashboardComponents/Navbar";
import UnplannedDowntimes from "@/components/DashboardComponents/UnplannedDowntimes";

export default {
  name: "UnplannedDowtimes_Dashboard",
  components: {
    Navbar,
    UnplannedDowntimes,
  }
}
</script>

<style scoped>

</style>