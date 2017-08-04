import * as _ from "lodash";

export class Helpers {
    /**
     * Updates target object by data object using specified rules.
     *
     * @param {T} target The target object to update.
     * @param {any} data The source data object.
     * @param {any} rules? The custom rules object.
     * @param {boolean} addAbsentFields? Indicates whether to add extra fields from data to target.
     * @returns Updated target object.
     */
    public static updateObject<T>(target: T, data: any, rules?: any, addAbsentFields?: boolean): T {
        if (!_.isPlainObject(data)) {
            return;
        }

        if (!_.isPlainObject(rules)) {
            rules = {};
        }

        var getKeyMap = function (obj: any): Array<any> {
            return _.map(_.keys(obj), (key: string) => {
                return {
                    key: key,
                    cc: _.camelCase(key),
                    lc: key.toLowerCase()
                }
            });
        }

        var getKey = function (key: string, map: Array<any>): string {
            var cc = _.camelCase(key),
                lc = key.toLowerCase();

            return (_.find(map, (m: any) => m.key == key)
                || _.find(map, (m: any) => m.cc == cc)
                || _.find(map, (m: any) => m.lc == lc)
                || {}).key;
        };

        var targetKeyMap = !!addAbsentFields ? getKeyMap(data) : getKeyMap(target),
            rulesKeyMap = getKeyMap(rules),
            dataKeys = _.keys(data);

        _.forEach(dataKeys, (dataKey: string) => {
            var targetKey = getKey(dataKey, targetKeyMap),
                ruleKey = getKey(dataKey, rulesKeyMap),
                value = _.get(data, dataKey);

            if (!!ruleKey) {
                if (_.isFunction(rules[ruleKey])) {
                    // when ruleKey is function - get function result as value.
                    // Example:
                    // { ruleKeyName: (item) => item + 2 }

                    var customValue = rules[ruleKey](value);
                    _.set(target, ruleKey, customValue);
                } else if (_.isString(rules[ruleKey])) {
                    // when ruleKey is string - get value of field with such name (from data object).
                    // Example:
                    // { ruleKeyName: 'someAnotherFieldName' }

                    if (_.some(dataKeys, (key: string) => key === rules[ruleKey])) {
                        var mappedValue = _.get(data, rules[ruleKey]);
                        _.set(target, ruleKey, mappedValue);
                    }
                } else if (_.isArray(rules[ruleKey]) && rules[ruleKey].length > 1) {
                    // when ruleKey is array - get all array items (except last) as names of fields of data object,
                    // and pass values of these fields as arguments for function (function is the last item in array).
                    // Example:
                    // ruleKeyName: [
                    //     'someFieldName1',
                    //     'someFieldName2',
                    //     'someFieldName3',
                    //     (field1, field2, field3) => field1 + field2 - field3
                    // ]

                    var ruleItems = rules[ruleKey] as Array<any>;
                    var args = [];

                    for (var i = 0; i < ruleItems.length; i++) {
                        if (i < ruleItems.length - 1) {
                            if (_.isString(ruleItems[i]) && _.some(dataKeys, (key: string) => key === ruleItems[i])) {
                                args.push(_.get(data, ruleItems[i]));
                            }
                        } else if (_.isFunction(ruleItems[i])) {
                            var customValue = ruleItems[i].apply(this, args);
                            _.set(target, ruleKey, customValue);
                        }
                    }
                }
            } else if (targetKey) {
                _.set(target, targetKey, value);
            }
        });

        return target;
    }
}
