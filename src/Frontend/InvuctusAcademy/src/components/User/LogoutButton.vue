<template>
    <q-item clickable v-ripple @click="logout">
        <q-item-section>
            <q-item-label>Выйти</q-item-label>
        </q-item-section>
    </q-item>
</template>

<script>
import { defineComponent } from "vue";
import { fetchlogout } from "boot/axios";

export default defineComponent({
    name: "logout-button",
    props: {
        logined: {
            type: Boolean
        }
    },
    methods: {
        logout() {
            try {
                fetchlogout();
                this.$emit("unautorize");
            } catch (e) {
                console.log(e.message);
            }
        },
    },
});
</script>
