<template>
  <div>
    <Navbar></Navbar>
    <UnplannedDowntimesShutdowns></UnplannedDowntimesShutdowns>
  </div>
</template>
<script>
import Navbar from "@/components/DashboardComponents/Navbar";
import UnplannedDowntimesShutdowns from "@/components/DashboardComponents/UnplannedDowntimesShutdowns";

export default {
  name: "UnplannedDowntimes_Shutdowns_Dashboard",
  components: {
    Navbar,
    UnplannedDowntimesShutdowns
  }
}
</script>

<style scoped>

</style>