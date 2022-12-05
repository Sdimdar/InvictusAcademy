<template>
    <div>
        <q-card :class="$attrs.class" class="my-card" flat bordered>
            <q-card-section horizontal class="flex justify-between">
                <q-card-section class="q-pt-xs">
                    <div class="text-h5 q-mt-sm q-mb-xs">{{ data.title }}</div>
                    <div class="text-caption text-grey">
                        {{ data.shortDescription }}
                    </div>
                </q-card-section>
            </q-card-section>
        </q-card>
    </div>
</template>

<script>
export default {
    props: {
        data: ""
    }
}
</script>

<style lang="scss" scoped>
    .course-img{
        height: 100px;
        width: 500px;
    }
</style>
