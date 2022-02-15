<template>
  <div>
    <Navbar></Navbar>
    <DowntimesReport></DowntimesReport>
  </div>

</template>

<script>
import Navbar from "@/components/DashboardComponents/Navbar";
import DowntimesReport from "@/components/DashboardComponents/DowntimesReport";

export default {
  name: "DowntimesDashboard",
  components: {
    Navbar,
    DowntimesReport,
  }
}
</script>

<style scoped>

</style>