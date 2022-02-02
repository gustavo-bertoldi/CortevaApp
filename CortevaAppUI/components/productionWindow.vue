<template>
    <!-- Production window -->
    <div class="d-flex production-window">
        <div class="d-flex title">
            <span>{{$t("productionWindow")}}</span>
        </div>
        <div class="d-flex interval-selection">
            <span>{{$t("from")}}</span>
            <input type="date" id="select-date-from" v-on:change="dateSelected();"/>
            <span>{{$t("to")}}</span>
            <input type="date" id="select-date-to" v-on:change="dateSelected();"/>
        </div>
    </div>
</template>

<script>
export default {
    name: "productionWindow",

    data() {
        const currentDate = new Date();
        let year = currentDate.getFullYear().toString();
        let month = currentDate.getMonth() + 1;
        if (month < 10) month = '0' + month.toString();
        else month = month.toString();

        let day = currentDate.getDate();
        if (day < 10) day = '0' + day.toString();
        else day = day.toString();

        let finalEndDate = year + '-' + month + '-' + day;
        let defaultStartDate = year + '-01-01';


        var data = {
            months: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            years: [],
            yearsAfterFrom: [],
            endDate: finalEndDate,
            startDate: defaultStartDate,
            firstDate: '2000-01-01'
        }

        //Populate years array
        for (let year = data.startYear; year <= data.currentYear; year++) data.years.push(year);
        data.yearsAfterFrom = data.years;

        return data;
    },

    methods: {
        calculateYearsAfterFrom: function () {
            const selectedYear = parseInt(document.getElementById('select-year-from').value);
            this.yearsAfterFrom = [];
            for (let i = selectedYear; i <= this.currentYear; i++) this.yearsAfterFrom.push(i);
        },

        dateSelected: function () {
            const dateFrom = document.getElementById('select-date-from').value;
            document.getElementById("select-date-to").setAttribute("min",dateFrom);
            const dateTo = document.getElementById('select-date-to').value;
            if (dateFrom && dateTo) this.yearSelectedFunction(dateFrom, dateTo);
        },

        showMenu: function () {
            document.querySelector("div.production-window").style.visibility = "";
        }
    },

    mounted() {
        if(sessionStorage.getItem("language") !== null){
            this.$i18n.locale = sessionStorage.getItem("language");
        }
        document.getElementById("select-date-from").setAttribute("min",this.firstDate);
        document.getElementById("select-date-from").setAttribute("max",this.endDate);
        document.getElementById("select-date-to").setAttribute("min",this.firstDate);
        document.getElementById("select-date-to").setAttribute("max",this.endDate);
        document.getElementById("select-date-from").setAttribute("value",this.startDate);
        document.getElementById("select-date-to").setAttribute("value",this.endDate);
    },

    computed: {

    },

    props: ['yearSelectedFunction']
}
</script>
<style scoped>
    div.production-window {
        flex-direction: column;
        border: solid 1px;
        border-radius: 5px;
        padding: 10px 5px;
        height: 91px;
    }

    div.production-window > div {
        justify-content: center;
    }

    div.production-window > div.title span {
        font-size: 20px;
        font-weight: bold;
        margin-bottom: 10px;
    }

    div.production-window > div.interval-selection > input {
        margin: 0px 10px;
        width: 35%;
        font-size: 12px;
    }

    div.production-window > div.interval-selection > * {
        font-size: 17px;
    }
</style>
